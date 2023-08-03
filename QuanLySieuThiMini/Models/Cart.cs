
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace QuanLySieuThiMini.Models
{
    public class Cart
    {
        private readonly ProductDBContext _dbContext;
        private readonly IServiceProvider _serviceProvider;


        public Cart(ProductDBContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
        }
        public string date { get; set; }

        public List<CartDetail> cartDetails { get; set; } = new List<CartDetail>();


        public void addToCart(int productID)
        {
            // Check if the product is already in the cart
            var cartItem = cartDetails.FirstOrDefault(cd => cd.proID == productID);
            if (cartItem != null)
            {
                cartItem.quantity++;
            }
            else
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ProductDBContext>();
                    var product = dbContext.Products.FirstOrDefault(p => p.proID == productID);
                        var item = new CartDetail
                        {
                            proID = product.proID,
                            proName = product.proName,
                            cost = product.cost,
                            quantity = 1
                        };
                        cartDetails.Add(item);
                }
            }
        }
        public void UpdateQuantity(int productId, int newQuantity)
        {
            // Find the cart item with the specified product ID
            var cartItem = cartDetails.FirstOrDefault(cd => cd.proID == productId);

            if (cartItem != null)
            {
                // Update the quantity if the item exists
                cartItem.quantity = newQuantity;
            }
            else
            {
                // Alternatively, you may choose to add the product to the cart if it doesn't exist
                // However, this depends on your business logic
                // Example: this.addToCart(productId);
            }
        }

        public void removeFromCart(int productID)
        {
            // Remove the product from the cart
            var cartItemToRemove = cartDetails.FirstOrDefault(cd => cd.proID == productID);
            if (cartItemToRemove != null)
            {
                cartDetails.Remove(cartItemToRemove);
            }
        }

        public Bill checkout()
        {
            // Create a new bill based on the cart details
            var bill = new Bill
            {
                date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                totalPrice = cartDetails.Sum(cd => cd.cost * cd.quantity),
                BillDetails = cartDetails.Select(cd => new BillDetail
                {
                    proName = cd.proName,
                    proID = cd.proID,
                    quantity = cd.quantity,
                    cost = cd.cost
                }).ToList()
            };

            // Clear the cart after checkout
            clearCart();

            return bill;
        }

        public void clearCart()
        {
            cartDetails.Clear();
        }

        public List<CartDetail> getCartDetail()
        {
            return cartDetails.ToList();
        }

    }
}