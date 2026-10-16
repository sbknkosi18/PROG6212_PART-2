using Microsoft.AspNetCore.Mvc;

namespace RaceDayAPI.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
