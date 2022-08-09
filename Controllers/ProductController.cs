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

        [Route("Product/{slug}")]
        public IActionResult Product(string slug)
        {
            ViewBag.Slug = slug;

            var products = _context.Products.Where(p => p.Slug == slug);

            ViewBag.Products = products;

            return View();
        }
    }
}
