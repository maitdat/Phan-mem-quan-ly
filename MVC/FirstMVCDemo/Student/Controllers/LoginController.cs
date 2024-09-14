using Microsoft.AspNetCore.Mvc;

namespace FirstMVC.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
