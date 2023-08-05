using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLySieuThiMini.Models;

namespace QuanLySieuThiMini.Controllers
{
    public class EmployeeController : Controller
    {
        DBHelper dbHelper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public EmployeeController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ProductDBContext dbContext)
        {
            dbHelper = new DBHelper(dbContext);
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        [Authorize(Roles = "Admin, Editor, Manager")]
        public IActionResult Index(int page = 1)
        {
            ViewBag.Positions = dbHelper.GetPositions();
            const int itemsPerPage = 6;
            List<EmployeeVM> empVM = dbHelper.GetEmployeeVM();

            int totalItems = empVM.Count;
            int skipItems = (page - 1) * itemsPerPage;

            List<EmployeeVM> vm = empVM.Skip(skipItems).Take(itemsPerPage).ToList();

            ViewData["TotalPages"] = (int)Math.Ceiling(totalItems / (double)itemsPerPage);
            ViewData["CurrentPage"] = page;

            return View(vm);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Editor, Manager")]
        public IActionResult Index(string? searchString, string posID , int page = 1)
        {
            ViewBag.Positions = dbHelper.GetPositions();
            List<EmployeeVM> empVM = dbHelper.SearchEmployeeVM(searchString, posID);

            const int itemsPerPage = 8;
            int totalItems = empVM.Count;
            int skipItems = (page - 1) * itemsPerPage;

            List<EmployeeVM> vm = empVM.Skip(skipItems).Take(itemsPerPage).ToList();
            ViewData["TotalPages"] = (int)Math.Ceiling(totalItems / (double)itemsPerPage);
            ViewData["CurrentPage"] = page;
            return View(vm);
        }

        [Authorize(Roles = "Admin, Editor, Manager")]
        public IActionResult Detail(int id)
        {
            Employee emp = dbHelper.DetailEmployee(id);
            ViewBag.Positions = dbHelper.GetPositions();
            EmployeeVM vm = new EmployeeVM()
            {
                empID = emp.empID,
                empName = emp.empName,
                empAddress = emp.empAddress,
                empPhone = emp.empPhone,
                age = emp.age,
                gender = emp.gender,
                posID = emp.posID,
                email = emp.email
            };
            return View(vm);
        }

        [Authorize(Roles = "Editor")]
        public IActionResult Create()
        {
            ViewBag.Positions = dbHelper.GetPositions();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Editor")]
        public IActionResult Create(EmployeeVM model)
        {
            ViewBag.Positions = dbHelper.GetPositions();
            if (ModelState.IsValid)
            {
                Employee emp = new Employee()
                {
                    empID = model.empID,
                    empName = model.empName,
                    empAddress = model.empAddress,
                    empPhone = model.empPhone,
                    age = model.age,
                    gender = model.gender,
                    posID = model.posID,
                    email = model.email
                };
                dbHelper.InsertEmployee(emp);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize(Roles = "Editor")]
        public IActionResult Update(int id)
        {
            ViewBag.Positions = dbHelper.GetPositions();
            Employee emp = dbHelper.DetailEmployee(id);
            EmployeeVM vm = new EmployeeVM()
            {
                empID = emp.empID,
                empName = emp.empName,
                empAddress = emp.empAddress,
                empPhone = emp.empPhone,
                age = emp.age,
                gender = emp.gender,
                posID = emp.posID,
                email = emp.email
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Editor")]
        public IActionResult Update(EmployeeVM model)
        {
            ViewBag.Positions = dbHelper.GetPositions();
            if (ModelState.IsValid)
            {
                Employee emp = new Employee()
                {
                    empID = model.empID,
                    empName = model.empName,
                    empAddress = model.empAddress,
                    empPhone = model.empPhone,
                    age = model.age,
                    gender = model.gender,
                    posID = model.posID,
                    email = model.email
                };
                dbHelper.UpdateEmployee(emp);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize(Roles = "Editor")]
        public IActionResult Delete(int id)
        {
            dbHelper.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
    }
}
