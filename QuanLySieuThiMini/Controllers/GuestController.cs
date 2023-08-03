using Microsoft.AspNetCore.Mvc;
using QuanLySieuThiMini.Models;

namespace QuanLySieuThiMini.Controllers
{
    public class GuestController : Controller
    {
        DBHelper dbHelper;

        public GuestController(ProductDBContext dbContext)
        {
            dbHelper = new DBHelper(dbContext);
        }
        public IActionResult Index(int page = 1)
        {
            const int itemsPerPage = 6;
            List<GuestVM> guestVM = dbHelper.GetGuestVM();

            int totalItems = guestVM.Count;
            int skipItems = (page - 1) * itemsPerPage;

            List<GuestVM> vm = guestVM.Skip(skipItems).Take(itemsPerPage).ToList();

            ViewData["TotalPages"] = (int)Math.Ceiling(totalItems / (double)itemsPerPage);
            ViewData["CurrentPage"] = page;

            return View(vm);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(GuestVM model)
        {
            if (ModelState.IsValid)
            {
                Guest guest = new Guest()
                {
                    guestPhone = model.guestPhone,
                    guestName = model.guestName
                };
                dbHelper.InsertGuest(guest);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Update(string phone)
        {
            Guest guest = dbHelper.DetailGuest(phone);
            GuestVM vm = new GuestVM()
            {
                guestPhone = guest.guestPhone, 
                guestName = guest.guestName
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Update(GuestVM model)
        {
            if (ModelState.IsValid)
            {
                Guest guest = new Guest()
                {
                    guestPhone = model.guestPhone,
                    guestName = model.guestName
                };
                dbHelper.UpdateGuest(guest);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Delete(string phone)
        {
            dbHelper.DeleteGuest(phone);
            return RedirectToAction("Index");
        }
    }
}
