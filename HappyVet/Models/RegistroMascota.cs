using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HappyVet.Repos.Models;

namespace HappyVet.Models
{
    [Table("Registro de Mascota")]
    public class RegistroMascota
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Display(Name = "Descripción")]
        [StringLength(50)]
        public string? Descripcion { get; set; }

        [NotMapped]
        public string DescripcionTipoAnimal
        {
            get
            {
                return string.Format("{0} - {1}", Descripcion, TipoAnimal.Descripcion);
            }
        }
        [Display(Name = "Imagen")]
        public string? ImagemMascota { get; set; }

        [Display(Name = "Tipo Animal")]
        public int? TipoAnimalRefId { get; set; }
        [ForeignKey("TipoAnimalRefId")]
        public virtual TipoAnimal? TipoAnimal { get; set; }

        [Display(Name = "Raza")]
        public int? RazaRefId { get; set; }
        [ForeignKey("RazaRefId")]
        public virtual Raza? Raza { get; set; }

        [Display(Name = "Tamaño")]
        public int? TamañoRefId { get; set; }
        [ForeignKey("TamañoRefId")]
        public virtual Tamaño? Tamaño { get; set; }


        [Display(Name = "Edad")]
        public int? EdadRefId { get; set; }
        [ForeignKey("EdadRefId")]
        public virtual Edad? Edad { get; set; }


        [Display(Name = "Fecha Ingreso")]
        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? FechaIngreso { get; set; }

        [Display(Name = "Fecha Registro")]
        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? FechaRegistro { get; set; }
    }
}

