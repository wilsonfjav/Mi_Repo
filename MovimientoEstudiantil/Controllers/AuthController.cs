using Microsoft.AspNetCore.Mvc;
using MovimientoEstudiantil.Models;
using MovimientoEstudiantil.Data;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;

namespace MovimientoEstudiantil.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly MovimientoEstudiantilContext _context;

        public AuthController(MovimientoEstudiantilContext context)
        {
            _context = context;
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Datos inválidos" });

            // Busca al usuario por correo (el correo sí está en texto plano)
            var usuario = _context.Usuarios.FirstOrDefault(u => u.correo == model.Correo);

            // Si no existe o la contraseña no es válida
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(model.Contrasena, usuario.contrasena))
                return Unauthorized(new { message = "Correo o contraseña incorrectos" });

            // Usuario autenticado correctamente
            return Ok(new
            {
                id = usuario.idUsuario,
                correo = usuario.correo,
                rol = usuario.rol
            });
        }

        // POST: api/Auth/logout
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Aquí podrías eliminar token, limpiar sesión, etc.
            return Ok(new { message = "Sesión cerrada correctamente." });
        }
    }
}
