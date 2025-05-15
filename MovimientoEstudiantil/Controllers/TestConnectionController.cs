using Microsoft.AspNetCore.Mvc;
using MovimientoEstudiantil.Data; // Asegúrate de que este sea el namespace correcto
using System.Linq;

namespace MovimientoEstudiantil.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestConnectionController : ControllerBase
    {
        private readonly MovimientoEstudiantilContext _context;

        public TestConnectionController(MovimientoEstudiantilContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var usuarios = _context.Usuarios.Take(5).ToList(); // Prueba si puede leer
                return Ok(new
                {
                    status = "Conexión exitosa",
                    cantidadUsuarios = usuarios.Count
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = "Error de conexión",
                    mensaje = ex.Message
                });
            }
        }
    }
}
