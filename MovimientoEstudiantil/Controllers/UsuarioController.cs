using Microsoft.AspNetCore.Mvc;

namespace MovimientoEstudiantil.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
