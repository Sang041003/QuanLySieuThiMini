using Microsoft.AspNetCore.Mvc;
using QuanLySieuThiMini.Models;

namespace QuanLySieuThiMini.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDBContext  _dbContext;

        public ProductController(ProductDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var products = _dbContext.Products.ToList();
            return PartialView("Index", products);
        }
    }
}
