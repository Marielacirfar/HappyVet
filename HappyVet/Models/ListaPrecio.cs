using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyVet.Models
{
    [Table("ListaPrecio")]
    public class ListaPrecio
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, ingresar la descripción")]
        [Display(Name = "Descripción")]
        [StringLength(50)]
        public string? Descripcion { get; set; }
               
        [Display(Name = "Vacuna")]
        public int? VacunaRefId { get; set; }
        [ForeignKey("VacunaRefId")]
        public virtual Vacuna? Vacuna { get; set; }

        [Required(ErrorMessage = "Por favor, ingresar el precio.")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Precio { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaRegistro { get; set; }
    }
}
