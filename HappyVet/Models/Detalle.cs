using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyVet.Models
{
    [Table("Detalle")]
    public class Detalle
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, ingresar la descripción")]
        [StringLength(50)]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Por favor, ingresar un porcentaje")]
        public int PorcentajeDescuento { get; set; }

        [Display(Name = "Vacuna")]
        public int? VacunaRefId { get; set; }
        [ForeignKey("VacunaRefId")]
        public virtual Vacuna? Vacuna { get; set; }

        [Display(Name = "Lista de Precio")]
        public int? ListaPrecioRefId { get; set; }
        [ForeignKey("ListaPrecioRefId")]
        public virtual ListaPrecio? ListaPrecio { get; set; }

        [Display(Name = "Precio")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TarifaPrecio { get; set; }

        [Display(Name = "Fecha Registro")]
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaRegistro { get; set; }
    }
}
