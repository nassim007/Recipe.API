using Microsoft.AspNetCore.Mvc;

namespace Recipe.API.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
