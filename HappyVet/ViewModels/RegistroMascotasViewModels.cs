using HappyVet.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyVet.ViewModels
{
    public class RegistroMascotasViewModels
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, ingresar la descripción.")]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }
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

        [Display(Name = "Imagem Mascota")]
        public IFormFile Imagem { get; set; }

        [Display(Name = "Imagen")]
        public string? ImagemMascota { get; set; }
    }
}
