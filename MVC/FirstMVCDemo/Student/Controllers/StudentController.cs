using Microsoft.AspNetCore.Mvc;

namespace Student.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult MaiTienDat()
        {
            return View();
        }
        public IActionResult FormInput()
        {
            return View();
        }
    }
}
