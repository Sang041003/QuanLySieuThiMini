using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLySieuThiMini.Models;
using System.Data;

namespace QuanLySieuThiMini.Controllers
{
    public class ShelfController : Controller
    {
        DBHelper dbHelper;

        public ShelfController(ProductDBContext dbContext)
        {
            dbHelper = new DBHelper(dbContext);
        }

        [Authorize(Roles = "Editor")]
        public IActionResult Index()
        {
            List<ShelfVM> list = dbHelper.GetShelfVM();
            return View(list);
        }
        [Authorize(Roles = "Editor")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Editor")]
        public IActionResult Create(ShelfVM vm)
        {
            if (ModelState.IsValid)
            {
                Shelf shelf = new Shelf()
                {
                    shelfID = vm.shelfID,
                    shelfLocation = vm.shelfLocation,
                };
                dbHelper.InsertShelf(shelf);
                return RedirectToAction("Index");
            }
            return View(vm);
        }
        [Authorize(Roles = "Editor")]
        public IActionResult Update(int id)
        {
            Shelf shelf = dbHelper.DetailShelf(id);
            ShelfVM vm = new ShelfVM()
            {
                shelfID = shelf.shelfID,
                shelfLocation = shelf.shelfLocation
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Editor")]
        public IActionResult Update(ShelfVM model)
        {
            if (ModelState.IsValid)
            {
                Shelf shelf = new Shelf()
                {
                    shelfID= model.shelfID,
                    shelfLocation = model.shelfLocation
                };
                dbHelper.UpdateShelf(shelf);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Authorize(Roles = "Editor")]
        public IActionResult Delete(int id)
        {
            dbHelper.DeleteShelf(id);
            return RedirectToAction("Index");
        }
    }
}
