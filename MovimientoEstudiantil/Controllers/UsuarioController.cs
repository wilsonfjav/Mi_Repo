using Microsoft.AspNetCore.Mvc; // Necesario para la funcionalidad del controlador.
using Microsoft.EntityFrameworkCore; // Para interactuar con la base de datos usando EF Core.
using MovimientoEstudiantil.Data; // Acceso al DbContext personalizado.
using MovimientoEstudiantil.Models; // Modelos de base de datos y DTOs.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovimientoEstudiantil.Controllers
{
    [ApiController] // Indica que esta clase es un controlador de API.
    [Route("api/[controller]")] // Define la ruta base para este controlador: api/Usuario.
    public class UsuarioController : ControllerBase // Hereda de ControllerBase ya que no usamos vistas.
    {
        private readonly MovimientoEstudiantilContext _context; // DbContext para acceso a la base de datos.

        public UsuarioController(MovimientoEstudiantilContext context)
        {
            _context = context; // Inyección de dependencias del contexto.
        }

        // GET: api/Usuario
        /// <summary>
        /// Obtiene todos los usuarios registrados.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            // Proyección: obtenemos solo las propiedades necesarias para el cliente.
            var usuarios = await _context.Usuarios
                .Select(u => new UsuarioDTO
                {
                    idUsuario = u.idUsuario,
                    correo = u.correo,
                    sede = u.sede,
                    rol = u.rol,
                    fechaRegistro = u.fechaRegistro
                })
                .ToListAsync();

            return Ok(usuarios); // Devuelve HTTP 200 con la lista.
        }

        // GET: api/Usuario/5
        /// <summary>
        /// Obtiene un usuario específico por su ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id)
        {
            var u = await _context.Usuarios.FindAsync(id); // Busca el usuario por ID.

            if (u == null)
                return NotFound(new { message = $"Usuario con ID {id} no encontrado." });

            // Proyección a DTO para evitar exponer datos sensibles.
            var usuarioDto = new UsuarioDTO
            {
                idUsuario = u.idUsuario,
                correo = u.correo,
                sede = u.sede,
                rol = u.rol,
                fechaRegistro = u.fechaRegistro
            };

            return Ok(usuarioDto); // Devuelve HTTP 200 con el usuario.
        }

        // POST: api/Usuario
        /// <summary>
        /// Registra un nuevo usuario.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> PostUsuario([FromBody] UsuarioCreateDTO usuarioInput)
        {
            if (!ModelState.IsValid) // Verifica que los datos sean válidos.
                return BadRequest(ModelState);

            // Verifica si el correo ya existe.
            if (await _context.Usuarios.AnyAsync(u => u.correo == usuarioInput.correo))
                return Conflict(new { message = "El correo ya está registrado." });

            // Crea la entidad nueva y hashea la contraseña.
            var nuevoUsuario = new Usuario
            {
                correo = usuarioInput.correo,
                sede = usuarioInput.sede,
                contrasena = BCrypt.Net.BCrypt.HashPassword(usuarioInput.contrasena),
                rol = usuarioInput.rol,
                fechaRegistro = DateTime.UtcNow
            };

            _context.Usuarios.Add(nuevoUsuario); // Agrega el usuario a la base de datos.
            await _context.SaveChangesAsync(); // Guarda los cambios.

            // Crea el DTO de respuesta.
            var dto = new UsuarioDTO
            {
                idUsuario = nuevoUsuario.idUsuario,
                correo = nuevoUsuario.correo,
                sede = nuevoUsuario.sede,
                rol = nuevoUsuario.rol,
                fechaRegistro = nuevoUsuario.fechaRegistro
            };

            // Devuelve HTTP 201 con ubicación y datos.
            return CreatedAtAction(nameof(GetUsuario), new { id = dto.idUsuario }, dto);
        }

        // PUT: api/Usuario/5
        /// <summary>
        /// Actualiza parcialmente un usuario existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] UsuarioUpdateDTO usuarioInput)
        {
            var usuario = await _context.Usuarios.FindAsync(id); // Busca el usuario por ID.

            if (usuario == null)
                return NotFound(new { message = $"Usuario con ID {id} no encontrado." });

            // Actualiza campos solo si se enviaron en el body.
            if (usuarioInput.sede > 0)
                usuario.sede = usuarioInput.sede;

            if (!string.IsNullOrWhiteSpace(usuarioInput.rol))
                usuario.rol = usuarioInput.rol;

            if (!string.IsNullOrWhiteSpace(usuarioInput.contrasena))
            {
                if (usuarioInput.contrasena.Length < 6)
                    return BadRequest(new { message = "La contraseña debe tener al menos 6 caracteres." });

                usuario.contrasena = BCrypt.Net.BCrypt.HashPassword(usuarioInput.contrasena);
            }

            try
            {
                await _context.SaveChangesAsync(); // Guarda los cambios.
                return NoContent(); // Devuelve HTTP 204 si todo salió bien.
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Usuarios.Any(e => e.idUsuario == id))
                    return NotFound(); // El usuario ya no existe.

                return StatusCode(500, new { message = "Error de concurrencia al actualizar el usuario." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error inesperado.", errorDetails = ex.Message });
            }
        }

        // DELETE: api/Usuario/5
        /// <summary>
        /// Elimina un usuario por su ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id); // Busca por ID.

            if (usuario == null)
                return NotFound(new { message = $"Usuario con ID {id} no encontrado." });

            _context.Usuarios.Remove(usuario); // Lo marca para eliminar.
            await _context.SaveChangesAsync(); // Ejecuta la eliminación.

            return NoContent(); // Devuelve HTTP 204 sin contenido.
        }
    }
}
