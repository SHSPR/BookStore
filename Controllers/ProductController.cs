using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        [Route("Product/{id}")]
        public IActionResult Product(int id)
        {
            ViewBag.ProductID = id;

            var products = _context.Products.Where(p => p.ProductID == id);

            ViewBag.Products = products;

            return View();
        }
    }
}
