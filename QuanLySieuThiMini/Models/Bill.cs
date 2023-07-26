
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public string guestPhone { get; set; }
        public Guest guest { get; set; }

        public int empID { get; set; }
        public Employee employee { get; set; }

        public ICollection<BillDetail> BillDetail { get; set; } = new List<BillDetail>();

        public void saveBill()
        {
        }

        public void printBill()
        {
        }

    }
}