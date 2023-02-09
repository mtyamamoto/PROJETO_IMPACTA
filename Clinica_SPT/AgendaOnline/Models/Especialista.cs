using System.ComponentModel.DataAnnotations;

namespace AgendaOnline.Models
{
    public class Especialista
    {
        [Key]
        public int EspecialistaId { get; set; }
        [Display(Name ="Nome do Especialista")]
        public string? EspecialistaNome { get; set; }
        [Display(Name ="Número do Conselho - CRM/CRP/... ")]

        public string CRM { get; set; }

        public int EspecialidadeId { get; set; }

        public Especialidade Especialidade { get; set; }
        public ICollection<Horario> Horarios { get; set; }
    }
}
