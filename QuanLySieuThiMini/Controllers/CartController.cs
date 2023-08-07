using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySieuThiMini.Models;

namespace QuanLySieuThiMini.Controllers
{
    [Authorize(Roles = "Seller")]
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly IServiceProvider _serviceProvider;
        DBHelper _dbHelper;

        public CartController(CartService cartService, IServiceProvider serviceProvider, DBHelper dbHelper)
        {
            _cartService = cartService;
            _serviceProvider = serviceProvider;
            _dbHelper = dbHelper;
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

        [HttpPost]
        public IActionResult Checkout(int empID, string guestPhone)
        {
            var cart = _cartService.GetCart();
            if(_dbHelper.DetailEmployee(empID)!=null && _dbHelper.DetailGuest(guestPhone) != null)
            {
                Bill bill = cart.checkout();
                bill.empID = empID;
                bill.guestPhone = guestPhone;
                _dbHelper.InsertBill(bill);
                foreach (BillDetail billDetail in bill.BillDetails.ToList())
                {
                    var product = _dbHelper.DetailProduct(billDetail.proID);

                    if (product != null && product.inventory >= billDetail.quantity)
                    {
                        product.inventory -= billDetail.quantity;
                        _dbHelper.UpdateProduct(product);
                    }
                }
                _cartService.ClearCart();
                return View(bill);
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid Employee ID or Guest Phone.";
                return View();
            }
        }
    }
}
