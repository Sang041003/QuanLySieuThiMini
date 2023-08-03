
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuanLySieuThiMini.Models
{
    public class CartDetail
    {

        public CartDetail()
        {
        }

        public int proID { get; set; }
        public Product product { get; set; }
        public string proName { get; set; }
        public int cost { get; set; }
        public int quantity { get; set; }

    }
}