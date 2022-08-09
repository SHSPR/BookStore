using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ItemsController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ItemsController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductModel> products = _context.Products;

            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                productModel.Slug = productModel.Title.ToLower().Replace(" ", "-");

                var slug = await _context.Products.FirstOrDefaultAsync(p => p.Slug == productModel.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "Такой товар уже существует");
                    return View(productModel);
                }

                if (productModel.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products/");
                    string imageName = Guid.NewGuid().ToString() + "_" + productModel.ImageUpload.FileName;

                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fstream = new FileStream(filePath, FileMode.Create);
                    await productModel.ImageUpload.CopyToAsync(fstream);
                    fstream.Close();

                    productModel.Cover = "/StaticFiles/" + imageName;
                }

                _context.Add(productModel);

                await _context.SaveChangesAsync();

                TempData["Success"] = "Товар создан";

                return RedirectToAction("Index");
            }

            return View(productModel);
        }

        //need find exact product via id
        public async Task<IActionResult> Edit(Guid id)
        {
            ProductModel productModel = await _context.Products.FindAsync(id);

            var product = _context.Products.Where(p => p.Id == id);

            ViewBag.Product = product;

            return View(productModel);
        }

        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    ProductModel productModel = await _context.Products.FindAsync(id);

        //    if (!string.Equals(productModel.Cover, "noimage.png")
        //    {

        //    }

        //    return View(productModel);
        //}
    }
}
