using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLySieuThiMini.Models;

namespace QuanLySieuThiMini.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly IServiceProvider _serviceProvider;

        public CartController(CartService cartService, IServiceProvider serviceProvider)
        {
            _cartService = cartService;
            _serviceProvider = serviceProvider;
        }

        public IActionResult AddToCart(int productID)
        {
            var cart = _cartService.GetCart();
            cart.addToCart(productID);
            _cartService.SaveCart(cart);
            return RedirectToAction("ViewCart", "Cart");
        }

        public IActionResult RemoveFromCart(int productID)
        {
            var cart = _cartService.GetCart();
            cart.removeFromCart(productID);
            _cartService.SaveCart(cart);
            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int newQuantity)
        {
            var cart = _cartService.GetCart();
            cart.UpdateQuantity(productId, newQuantity);
            return RedirectToAction("ViewCart");
        }

        public IActionResult ViewCart()
        {
            var cart = _cartService.GetCart();
            return View(cart);
        }

        public IActionResult Checkout()
        {
            var cart = _cartService.GetCart();
            var bill = cart.checkout();
            _cartService.ClearCart();
            return View(bill);
        }
    }
}
