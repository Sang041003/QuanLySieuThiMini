using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySieuThiMini.Models;

namespace QuanLySieuThiMini.Controllers
{
    public class StatisticController : Controller
    {
        DBHelper dbHelper;
        ProductDBContext context;

        public StatisticController(DBHelper dbHelper, ProductDBContext context)
        {
            this.dbHelper = dbHelper;
            this.context = context;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string startDate, string endDate)
        {
            List<BillStatistics> revenueByDateRange = dbHelper.GetRevenueByDateRange(startDate,endDate);
            var viewModel = revenueByDateRange.Select(stat => new BillStatisticsVM
            {
                Date = stat.Date,
                TotalPrice = stat.TotalPrice
            }).ToList();

            return View(viewModel);
        }

       
    }
}
