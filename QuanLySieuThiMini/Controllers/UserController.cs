using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySieuThiMini.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace QuanLySieuThiMini.Controllers
{
    public class UserController : Controller
    {
        DBHelper dbHelper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ProductDBContext dbContext, SignInManager<IdentityUser> signInManager)
        {
            dbHelper = new DBHelper(dbContext);
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            List<IdentityUser> users = dbHelper.GetIdentityUsers();
            return View(users);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(IdentityUser userModel, string password)
        {
            if (ModelState.IsValid)
            {
                if (dbHelper.EmaillExists(userModel.Email))
                {
                    var result = await userManager.CreateAsync(userModel, password);

                    if (result.Succeeded)
                    {
                        userModel.EmailConfirmed = true;
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(userModel);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Update(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = dbHelper.DetailUser(id);

            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string id, IdentityUser userModel, string? newPassword)
        {
            if (id != userModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await userManager.FindByIdAsync(id);
                    user.UserName = userModel.UserName;
                    user.Email = userModel.Email;
                    await userManager.UpdateAsync(user);

                    if (!string.IsNullOrEmpty(newPassword))
                    {
                        var token = await userManager.GeneratePasswordResetTokenAsync(user);
                        var result = await userManager.ResetPasswordAsync(user, token, newPassword);

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            return View(userModel);
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!dbHelper.UserModelExists(userModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Disable(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = dbHelper.DetailUser(id);

            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Disable(IdentityUser userModel)
        {
            var model = await userManager.FindByIdAsync(userModel.Id);

            if (model == null)
            {
                return NotFound();
            }

            await userManager.SetLockoutEndDateAsync(model, DateTimeOffset.MaxValue);

            return RedirectToAction("Index", "User");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Enable(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = dbHelper.DetailUser(id);

            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Enable(IdentityUser userModel)
        {
            var model = await userManager.FindByIdAsync(userModel.Id);

            if (model == null)
            {
                return NotFound();
            }

            // Đặt thời gian chặn (lockout) về null để Enable người dùng
            await userManager.SetLockoutEndDateAsync(model, null);

            return RedirectToAction("Index","User");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRole(string userId, string roleName)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            if (!await roleManager.RoleExistsAsync(roleName))
            {
                return NotFound();
            }

            await userManager.AddToRoleAsync(user, roleName);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveRole(string userId, string roleName)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            if (!await roleManager.RoleExistsAsync(roleName))
            {
                return NotFound();
            }

            await userManager.RemoveFromRoleAsync(user, roleName);

            return RedirectToAction(nameof(Index));
        }


        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home"); // Change "Home" to the appropriate controller and action after login
                    }
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Login", "User");
        }
    }
}
