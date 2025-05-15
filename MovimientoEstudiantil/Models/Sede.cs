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

        [Required]
        [Column("provincia_id")]
        public int idProvincia { get; set; }

        public virtual Provincia Provincia { get; set; }
    }
}
