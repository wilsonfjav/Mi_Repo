using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovimientoEstudiantil.Data;
using MovimientoEstudiantil.Models;

namespace MovimientoEstudiantil.Controllers
{
    // Define que esta clase es un controlador API y su ruta base será "[controller]" (Estudiante)
    [ApiController]
    [Route("[controller]")]
    public class EstudianteController : ControllerBase
    {
        // Inyección del contexto de base de datos
        public readonly MovimientoEstudiantilContext _context;

        public EstudianteController(MovimientoEstudiantilContext context)
        {
            _context = context;
        }

        //------------------------------------------------------------------------//
        // GET: /Estudiante/Lista_Estudiantes
        // Retorna todos los estudiantes de la base de datos
        [HttpGet("Lista_Estudiantes")]
        public async Task<List<Estudiante>> ListaEstudiantes()
        {
            var lista = await _context.Estudiantes.ToListAsync();
            return lista;

        }//end method 

        //------------------------------------------------------------------------//
        // GET: /Estudiante/BuscarEstudiante/{id}
        // Retorna un estudiante específico por su ID
        [HttpGet("BuscarEstudiante/{id}")]
        public async Task<Estudiante> Buscar(int id)
        {
            var temp = await _context.Estudiantes.FirstOrDefaultAsync(x => x.idEstudiante == id);
            return temp;
        }//end method

        //------------------------------------------------------------------------//
        // DELETE: /Estudiante/EliminarEstudiante/{id}
        // Elimina un estudiante por su ID
        [HttpDelete("EliminarEstudiante/{id}")]
        public async Task<string> Eliminar(int id)
        {
            string mensaje = "No se existe el estudiante";

            try
            {
                var temp = await _context.Estudiantes.FirstOrDefaultAsync(x => x.idEstudiante == id);
                if (temp != null)
                {
                    _context.Estudiantes.Remove(temp);
                    await _context.SaveChangesAsync();// Guardar cambios
                    mensaje = "Estudiante " + temp.idEstudiante + " eliminado correctamente";
                }
            }
            catch (Exception ex)
            {
                mensaje += ex.InnerException.Message;

            }
            return mensaje;

        }//end method delete 

        //------------------------------------------------------------------------//
        // POST: /Estudiante/AgregarEstudiante
        // Agrega un nuevo estudiante luego de validarlo
        [HttpPost("AgregarEstudiante")]
        public async Task<string> Agregar(Estudiante estudiante)
        {
            string mensaje = "";

            // Validación completa del estudiante
            var error = await ValidarEstudianteAsync(estudiante);
            if (error != null)
                return error;

            try
            {
                _context.Estudiantes.Add(estudiante);// Agrega a la base
                await _context.SaveChangesAsync();// Guardar cambios
                mensaje = $"Estudiante {estudiante.idEstudiante} fue almacenado correctamente...";

            }
            catch (Exception ex)
            {
                mensaje = "Error " + ex.InnerException.Message;

            }
            return mensaje;
        }//end methon add 

        //------------------------------------------------------------------------//
        // PUT: /Estudiante/ModificarEstudiante/5
        // Modifica un estudiante existente, si se encuentra
        [HttpPut("ModificarEstudiante/{id}")]
        public async Task<string> Modificar(int id, Estudiante estudiante)
        {
            string mensaje = $"No se logró aplicar los cambios al estudiante {estudiante.idEstudiante}\n";

            // Validar que el id de la ruta coincida con el del objeto
            if (id != estudiante.idEstudiante)
            {
                return $"Error: El ID de la ruta ({id}) no coincide con el ID del estudiante ({estudiante.idEstudiante}).";
            }

            // Validación completa del estudiante
            var error = await ValidarEstudianteAsync(estudiante);
            if (error != null) return error;

            try
            {
                // Verificar si el estudiante existe en la base de datos
                var estudianteExistente = await _context.Estudiantes
                    .FirstOrDefaultAsync(e => e.idEstudiante == estudiante.idEstudiante);

                if (estudianteExistente == null)
                {
                    return $"Error: No existe el estudiante con ID {estudiante.idEstudiante}.";
                }

                // Si existe, actualizamos sus campos
                estudianteExistente.correo = estudiante.correo;
                estudianteExistente.provinciaId = estudiante.provinciaId;
                estudianteExistente.sedeId = estudiante.sedeId;
                estudianteExistente.satisfaccionCarrera = estudiante.satisfaccionCarrera;
                estudianteExistente.anioIngreso = estudiante.anioIngreso;

                await _context.SaveChangesAsync();// Guardar cambios
                mensaje = $"Los cambios al estudiante {estudiante.idEstudiante} se aplicaron correctamente.";
            }
            catch (Exception ex)
            {
                mensaje = "Error: " + ex.InnerException?.Message ?? ex.Message;
            }
            return mensaje;
        }//end method edit 


        //------------------------------------------------------------------------//
        // Método privado para validar reglas de negocio antes de guardar/modificar un estudiante
        private async Task<string?> ValidarEstudianteAsync(Estudiante estudiante)
        {
            // Validar año
            if (estudiante.anioIngreso != 2022 &&
                estudiante.anioIngreso != 2023 &&
                estudiante.anioIngreso != 2024)
            {
                return "Error: El año de ingreso debe ser 2022, 2023 o 2024.";
            }

            //-------------------------------
            // Validar satisfacción
            if (string.IsNullOrWhiteSpace(estudiante.satisfaccionCarrera))
            {
                return "Error: La satisfacción no puede estar vacía.";
            }

            var satisfaccion = estudiante.satisfaccionCarrera.Trim().ToLower();
            if (satisfaccion != "si" && satisfaccion != "no")
            {
                return "Error: La satisfacción debe ser 'Si' o 'No'.";
            }
            //-------------------------------

            // Validar correo
            if (string.IsNullOrWhiteSpace(estudiante.correo))
            {
                return "Error: El correo electrónico es obligatorio.";
            }

            var correo = estudiante.correo.Trim().ToLower();
            if (!correo.EndsWith("@ucr.ac.cr"))
            {
                return "Error: Solo se permiten correos del dominio '@ucr.ac.cr'.";
            }

            //-------------------------------
            // Validar existencia de provincia
            var provinciaExiste = await _context.Provincias.AnyAsync(p => p.idProvincia == estudiante.provinciaId);
            if (!provinciaExiste)
            {
                return $"Error: La provincia con ID {estudiante.provinciaId} no existe.";
            }

            // Validar existencia de sede
            var sedeExiste = await _context.Sedes.AnyAsync(s => s.idSede == estudiante.sedeId);
            if (!sedeExiste)
            {
                return $"Error: La sede con ID {estudiante.sedeId} no existe.";
            }

            return null; // Todo está bien
        }

    }//end block the class

}//end namespaces