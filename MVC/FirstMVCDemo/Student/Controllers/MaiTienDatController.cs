using Microsoft.AspNetCore.Mvc;

namespace FirstMVC.Controllers
{
    public class MaiTienDatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FormInput()
        {
            return View();
        }
    }
}
