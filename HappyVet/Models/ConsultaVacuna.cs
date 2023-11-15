using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HappyVet.Models
{
    public class ConsultaVacuna
    {
        public int Id { get; set; }

        [Display(Name = "Vacuna")]
        [Required(ErrorMessage = "Por favor, seleccione una vacuna.")]
        public int? VacunaRefId { get; set; }

        [ForeignKey("VacunaRefId")]
        public virtual Vacuna? Vacuna { get; set; }

        public int? ConsultaId { get; set; }
        [ForeignKey("ConsultaId")]

        public DateTime Created { get; set; } = DateTime.Now;

    }
}
