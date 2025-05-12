using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovimientoEstudiantil.Models
{
    public class Grafico
    {
        [Key]
        public int idGrafico { get; set; }

        [Required(ErrorMessage = "El tipo de gráfico es obligatorio")]
        public string tipo { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int idUsuario { get; set; }

        [Required(ErrorMessage = "El filtro es obligatorio")]
        public int filtroId { get; set; }

        [Required(ErrorMessage = "El formato de exportación es obligatorio")]
        public string formatoExportacion { get; set; }

        // Propiedad de navegación (opcional pero recomendada)
        public Usuario Usuario { get; set; }
    }
}
