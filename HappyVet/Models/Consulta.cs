using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyVet.Models
{
    [Table("Consulta")]
    public class Consulta
    {
        public int Id { get; set; }

        [Display(Name = "Fecha/Hora Consulta")]
        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Por favor, ingrese fecha y hora para la consulta.")]
        public DateTime? FechaHoraConsulta { get; set; }

        [Display(Name = "RegistroMascota")]
        [Required(ErrorMessage = "Por favor, seleccione un registro de Mascota.")]
        public int? RegistroMascotaRefId { get; set; }
        [ForeignKey("RegistroMascotaRefId")]
        public virtual RegistroMascota? RegistroMascota { get; set; }

        public virtual List<ConsultaVacuna> Vacuna { get; set; }

        public DateTime? FechaRegistro { get; set; } = DateTime.Now;
        [NotMapped]
        public string? ValidationError { get; set; }

        public int NumberOfTarifas
        {
            get => Vacuna.Count;
        }

        public Consulta()
        {
            Vacuna = new List<ConsultaVacuna>();
        }

    }
}
