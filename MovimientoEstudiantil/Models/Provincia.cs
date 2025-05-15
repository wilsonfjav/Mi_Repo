using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovimientoEstudiantil.Models
{
    [Table("Provincia")]
    public class Provincia
    {
        [Key]
        [Column("id_provincia")]
        public int idProvincia { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        // Relación con Sede (una provincia tiene muchas sedes)
        public virtual ICollection<Sede> Sedes { get; set; }
    }
}
