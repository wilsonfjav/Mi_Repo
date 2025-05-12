using Microsoft.AspNetCore.Mvc;

namespace MovimientoEstudiantil.Controllers
{
    public class EstudianteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
