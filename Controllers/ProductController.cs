using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class ProductController : Controller
    {
        ProductContext productContext = new ProductContext();

        [Route("Product/{id}")]
        public IActionResult Product(int id)
        {
            ViewBag.ProductID = id;

            var products = productContext.Products.Where(p => p.ProductID == id);

            ViewBag.Products = products;

            return View();
        }
    }
}
