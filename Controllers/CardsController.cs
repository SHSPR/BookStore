using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CardsController : Controller
    {
        [Route("cards/{id}")]
        public IActionResult Index(int id)
        {
            return View();
        }
    }
}
