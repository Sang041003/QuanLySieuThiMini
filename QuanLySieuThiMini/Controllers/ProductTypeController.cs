using Microsoft.AspNetCore.Mvc;
using QuanLySieuThiMini.Models;

namespace QuanLySieuThiMini.Controllers
{
    public class ProductTypeController : Controller
    {
        DBHelper dbHelper;

        public ProductTypeController(ProductDBContext dbContext)
        {
            dbHelper = new DBHelper(dbContext);
        }
        public IActionResult Index()
        {
            List<ProductTypeVM> list = dbHelper.GetProductTypeVM();
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductTypeVM productTypeVM)
        {
            if (ModelState.IsValid)
            {
                ProductType type = new ProductType()
                {
                    typeID = productTypeVM.typeID,
                    typeName = productTypeVM.typeName
                };
                dbHelper.InsertProductType(type);
                return RedirectToAction("Index");
            }
            return View(productTypeVM);
        }
        public IActionResult Update(string id)
        {
            ProductType cate = dbHelper.DetailProductType(id);
            ProductTypeVM vm = new ProductTypeVM()
            {
                typeID = cate.typeID,
                typeName = cate.typeName
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Update(ProductTypeVM model)
        {
            if (ModelState.IsValid)
            {
                ProductType cate = new ProductType()
                {
                    typeID = model.typeID,
                    typeName = model.typeName
                };
                dbHelper.UpdateProductType(cate);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Delete(string id)
        {
            dbHelper.DeleteProductType(id);
            return RedirectToAction("Index");
        }
    }
}
