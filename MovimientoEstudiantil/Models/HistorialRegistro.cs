using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovimientoEstudiantil.Models
{
    public class HistorialRegistro
    {
        [Key]
        public int idHistorial { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int idUsuario { get; set; }

        [Required(ErrorMessage = "La acción es obligatoria")]
        public string accion { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string descripcion { get; set; }

        [Required]
        public DateTime fechaRegistro { get; set; } = DateTime.Now;

        [Required]
        public TimeSpan hora { get; set; } = DateTime.Now.TimeOfDay;

        [Required(ErrorMessage = "El rol es obligatorio")]
        public string rol { get; set; }

        // Propiedad de navegación
        public Usuario Usuario { get; set; }
    }
}
