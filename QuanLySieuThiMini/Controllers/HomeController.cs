using Microsoft.AspNetCore.Mvc;
using QuanLySieuThiMini.Models;
using System.Diagnostics;

namespace QuanLySieuThiMini.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        DBHelper dbHelper;
        public HomeController(ILogger<HomeController> logger, DBHelper dbHelper)
        {
            _logger = logger;
            this.dbHelper = dbHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}