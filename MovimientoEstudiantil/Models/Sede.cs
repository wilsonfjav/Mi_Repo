using MovimientoEstudiantil.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MovimientoEstudiantil.Models
{
    [Table("Sede")]
    public class Sede
    {
        [Key]
        [Column("id_sede")]
        public int id { get; set; }

        [Required]
        [Column("nombre")]
        [StringLength(100)]
        public string nombre { get; set; }

        [Required]
        [Column("provincia_id")]
        [ForeignKey("provinciaId")]
        public int provinciaId { get; set; }

    }
}