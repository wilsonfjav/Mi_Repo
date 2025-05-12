using Microsoft.AspNetCore.Mvc;

namespace MovimientoEstudiantil.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
