using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLySieuThiMini.Models;
using System.Data;

namespace QuanLySieuThiMini.Controllers
{
    public class PositionController : Controller
    {
        DBHelper dbHelper;

        public PositionController(ProductDBContext dbContext)
        {
            dbHelper = new DBHelper(dbContext);
        }

        [Authorize(Roles = "Editor")]
        public IActionResult Index()
        {
            List<PositionVM> list = dbHelper.GetPositionVM();
            return View(list);
        }
        [Authorize(Roles = "Editor")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Editor")]
        public IActionResult Create(PositionVM vm)
        {
            if (ModelState.IsValid)
            {
                Position pos = new Position()
                {
                    posID = vm.posID,
                    posName = vm.posName
                };
                dbHelper.InsertPosition(pos);
                return RedirectToAction("Index");
            }
            return View(vm);
        }
        [Authorize(Roles = "Editor")]
        public IActionResult Update(string id)
        {
            Position pos = dbHelper.DetailPosition(id);
            PositionVM vm = new PositionVM()
            {
                posID = pos.posID,
                posName = pos.posName
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Editor")]
        public IActionResult Update(PositionVM model)
        {
            if (ModelState.IsValid)
            {
                Position pos = new Position()
                {
                    posID= model.posID,
                    posName= model.posName
                };
                dbHelper.UpdatePosition(pos);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Authorize(Roles = "Editor")]
        public IActionResult Delete(string id)
        {
            dbHelper.DeletePosition(id);
            return RedirectToAction("Index");
        }
    }
}
