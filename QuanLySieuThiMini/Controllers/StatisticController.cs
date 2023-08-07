using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySieuThiMini.Models;

namespace QuanLySieuThiMini.Controllers
{
    [Authorize(Roles ="Manager")]
    public class StatisticController : Controller
    {
        DBHelper dbHelper;

        public StatisticController(DBHelper dbHelper)
        {
            this.dbHelper = dbHelper;
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
