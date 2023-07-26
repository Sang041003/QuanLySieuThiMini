
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace QuanLySieuThiMini.Models
{
    public class Cart
    {

        public Cart()
        {
        }
        public int cartID { get; set; }

        public string date { get; set; }

        public ICollection<CartDetail> cartDetails { get; set; } = new List<CartDetail>();

        public void addToCart(int productID)
        {
        }

        public void removeFromCart()
        {
        }

        public Bill checkout()
        {
            return null;
        }

        public void clearCart()
        {
        }

        public List<CartDetail> getCartDetail()
        {
            return null;
        }

    }
}