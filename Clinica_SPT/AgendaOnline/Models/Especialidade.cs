using System.ComponentModel.DataAnnotations;

namespace AgendaOnline.Models
{
    public class Especialidade
    {
        [Key]
        public int EspecialidadeId { get; set; }
        [Display(Name ="Especialidade:")]
        public string? EspecialidadeNome { get; set; }
        public virtual ICollection<Especialista> Especialistas { get; set; }
    }
}
