using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuanLySieuThiMini.Models;
using System.Runtime.InteropServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace QuanLySieuThiMini.Controllers
{
    public class ProductController : Controller
    {
        DBHelper dbHelper;

        public ProductController(ProductDBContext dbContext)
        {
            dbHelper = new DBHelper(dbContext);
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.Types = dbHelper.GetProductTypes();
            const int itemsPerPage = 5;
            List<ProductVM> productVMs = dbHelper.GetProductVM();

            int totalItems = productVMs.Count;
            int skipItems = (page - 1) * itemsPerPage;

            List<ProductVM> vm = productVMs.Skip(skipItems).Take(itemsPerPage).ToList();

            ViewData["TotalPages"] = (int)Math.Ceiling(totalItems / (double)itemsPerPage);
            ViewData["CurrentPage"] = page;

            return View(vm);
        }
        [HttpPost]
        public IActionResult Index(string? searchString, string? typeID, int page = 1)
        {
            ViewBag.Types = dbHelper.GetProductTypes();
            List<ProductVM> productVMs = dbHelper.SearchProductVM(searchString, typeID);

            const int itemsPerPage = 8;
            int totalItems = productVMs.Count;
            int skipItems = (page - 1) * itemsPerPage;

            List<ProductVM> vm = productVMs.Skip(skipItems).Take(itemsPerPage).ToList();
            ViewData["TotalPages"] = (int)Math.Ceiling(totalItems / (double)itemsPerPage);
            ViewData["CurrentPage"] = page;
            return View(vm);
        }
        public IActionResult Detail(int id)
        {
            Product product = dbHelper.DetailProduct(id);
            ViewBag.Types = dbHelper.GetProductTypes();
            ViewBag.Shelves = dbHelper.GetShelves();
            ProductVM vm = new ProductVM()
            {
                proID = product.proID,
                proName = product.proName,
                typeID = product.typeID,
                cost = product.cost,
                inventory = product.inventory,
                shelfID = product.shelfID
            };
            return View(vm);
        }

        [Authorize(Roles ="Editor")]
        public IActionResult Create()
        {
            ViewBag.Types = dbHelper.GetProductTypes();
            ViewBag.Shelves = dbHelper.GetShelves();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Editor")]
        public IActionResult Create(ProductVM model)
        {
            ViewBag.Types = dbHelper.GetProductTypes();
            ViewBag.Shelves = dbHelper.GetShelves();
            if (ModelState.IsValid)
            {
                Product pro = new Product()
                {
                    proName = model.proName,
                    typeID = model.typeID,
                    cost = model.cost,
                    inventory = model.inventory,
                    shelfID = model.shelfID
                };
                dbHelper.InsertProduct(pro);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize(Roles = "Editor")]
        public IActionResult Update(int id)
        {
            ViewBag.Types = dbHelper.GetProductTypes();
            ViewBag.Shelves = dbHelper.GetShelves();
            Product product = dbHelper.DetailProduct(id);
            ProductVM vm = new ProductVM()
            {
                proID = product.proID,
                proName = product.proName,
                typeID = product.typeID,
                cost = product.cost,
                inventory = product.inventory,
                shelfID = product.shelfID
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Editor")]
        public IActionResult Update(ProductVM model)
        {
            ViewBag.Types = dbHelper.GetProductTypes();
            ViewBag.Shelves = dbHelper.GetShelves();
            if (ModelState.IsValid)
            {
                Product pro = new Product()
                {
                    proID = model.proID,
                    proName = model.proName,
                    typeID = model.typeID,
                    cost = model.cost,
                    inventory = model.inventory,
                    shelfID = model.shelfID
                };
                dbHelper.UpdateProduct(pro);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Authorize(Roles = "Editor")]
        public IActionResult Delete(int id)
        {
            dbHelper.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
