using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovimientoEstudiantil.Models
{
    [Table("Provincia")]
    public class Provincia
    {
        [Key]
        [Column("id_provincia")]
        public int idProvincia { get; set; }

        [Required]
        [Column("nombre")]
        [StringLength(50)]
        public string nombre { get; set; }

        public ICollection<Sede> Sedes { get; set; }
    }
}
