using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovimientoEstudiantil.Models
{
    [Table("Estudiante")]
    public class Estudiante
    {
        [Key]
        [Column("id_estudiante")]
        public int idEstudiante { get; set; }

        // Campo obligatorio, columna "correo", validado como dirección de correo, máx 100 caracteres
        [Required]
        [Column("correo")]
        [EmailAddress]
        [StringLength(100)]
        public string correo { get; set; }

        [Required]
        [Column("provincia_id")]
        public int provincia { get; set; }

        [Required]
        [Column("sede_id")]
        public int sede { get; set; }

        [Required]
        [Column("satisfaccion_carrera")]
        [StringLength(2)]
        public string satisfaccionCarrera { get; set; }

        [Required]
        [Column("anioIngreso")]
        public int anioIngreso { get; set; }

        public virtual Provincia Provincia { get; set; }

        public virtual Sede Sede { get; set; }

    }
}
