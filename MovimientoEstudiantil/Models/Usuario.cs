using System;
using System.ComponentModel.DataAnnotations;

namespace MovimientoEstudiantil.Models
{
    public class Usuario
    {
        [Key]
        public int idUsuario { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
        public string correo { get; set; }

        [Required(ErrorMessage = "La sede es obligatoria")]
        public string sede { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public string contrasena { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio")]
        public string rol { get; set; }

        [Required]
        public DateTime fechaRegistro { get; set; } = DateTime.Now;  // Valor por defecto
    }
}
