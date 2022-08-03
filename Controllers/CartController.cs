using BookStore.Infrastructure;
using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext _context;
        public CartController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartVM = new()
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price),
            };

            return View(cartVM);
        }
        public async Task<IActionResult> Add(long id)
        {
            ProductModel productModel = await _context.Products.FindAsync(id);

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartItem cartItem = cart.Where(c => c.ItemId == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(productModel));
            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);

            TempData["Success"] = "Товар Добавлен В Корзину";

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> Decreace(long id)
        {
            ProductModel productModel = await _context.Products.FindAsync(id);

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartItem cartItem = cart.Where(c => c.ItemId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity -= 1;
            }
            else if (cartItem.Quantity == 1)
            {
                cart.Remove(cartItem);
            }

            HttpContext.Session.SetJson("Cart", cart);

            TempData["Error"] = "Товар Удален";

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Clear()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            cart.Clear();

            HttpContext.Session.SetJson("Cart", cart);

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
