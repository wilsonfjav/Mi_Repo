using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovimientoEstudiantil.Models
{
    public class Estudiante
    {
        [Key]
        public int idEstudiante { get; set; }

        [Required(ErrorMessage = "El nombre del estudiante es obligatorio")]
        public string nombre { get; set; }  // Se agregó campo de nombre, que estaba en el mensaje pero no en el código.
        [ForeignKey("Provincia")]
        [Required(ErrorMessage = "La provincia es obligatoria")]
        public int provincia { get; set; }
        [ForeignKey("Provincia")]
        [Required(ErrorMessage = "La sede es obligatoria")]
        public int sede { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
        public string correo { get; set; }

        [Required(ErrorMessage = "La satisfacción es obligatoria")]
        public string satisfaccionCarrera { get; set; }

        [Required(ErrorMessage = "El año de ingreso es obligatorio")]
        public int anioIngreso { get; set; }
    }
}