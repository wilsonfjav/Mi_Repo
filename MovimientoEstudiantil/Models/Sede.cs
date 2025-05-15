using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovimientoEstudiantil.Models
{
    [Table("Sede")]
    public class Sede
    {
        [Key]
        public int idSede { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        // Clave foránea
        [ForeignKey("Provincia")]
        public int idProvincia { get; set; }

        public virtual Provincia Provincia { get; set; }
    }
}
