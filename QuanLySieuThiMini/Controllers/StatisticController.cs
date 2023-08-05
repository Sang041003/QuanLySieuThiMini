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

        public IActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            // Nếu không có ngày bắt đầu và ngày kết thúc được chọn, mặc định là hiện tại và một tuần trước đó
            if (startDate == null)
                startDate = DateTime.Now.Date.AddDays(-7);

            if (endDate == null)
                endDate = DateTime.Now.Date;

            // Lấy dữ liệu các hóa đơn từ CSDL trong khoảng thời gian đã chọn
            var bills = context.Bills
           .Where(b => DateTime.Parse(b.date) >= startDate && DateTime.Parse(b.date) <= endDate)
           .ToList();

            // Thực hiện các phép tính thống kê
            int totalRevenue = bills.Sum(b => b.totalPrice);
            Dictionary<string, int> revenueByDate = bills
                .GroupBy(b => b.date)
                .ToDictionary(group => group.Key, group => group.Sum(b => b.totalPrice));

            // Truyền dữ liệu thống kê và các ngày đã chọn tới View
            ViewBag.TotalRevenue = totalRevenue;
            ViewBag.RevenueByDate = revenueByDate;
            ViewBag.StartDate = startDate.Value.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate.Value.ToString("yyyy-MM-dd");

            return View();
        }
    }
}
