
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuanLySieuThiMini.Models
{
    [PrimaryKey(nameof(billID), nameof(proID))]
    public class BillDetail
    {

        public BillDetail()
        {
        }

        public string billID { get; set; }
        public Bill bill { get; set; }

        public string billName { get; set; }

        public int proID { get; set; }
        public Product product { get; set; }

        public int quantity { get; set; }

        public int totalCost()
        {
            return 0;
        }

    }
}