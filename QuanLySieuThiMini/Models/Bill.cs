
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace QuanLySieuThiMini.Models
{
    public class Bill
    {
        public Bill()
        {
        }

        [Key]
        public string billID { get; set; }

        public string date { get; set; }

        public int totalPrice { get; set; }

        [ForeignKey("Guest")]
        public string guestPhone { get; set; }
        public Guest Guest { get; set; }

        [ForeignKey("Employee")]
        public int empID { get; set; }
        public Employee Employee { get; set; }

        public ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();

        public void saveBill()
        {
        }

        public void printBill()
        {
        }
    }
}