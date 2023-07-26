
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

        public int cartID { get; set; }
        public Cart cart { get; set; }

        public int proID { get; set; }
        public Product product { get; set; }

        public int quantity { get; set; }
        public void updateQuantity()
        {
        }

    }
}