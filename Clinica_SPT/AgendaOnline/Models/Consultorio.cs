using System.ComponentModel.DataAnnotations;
namespace AgendaOnline.Models
{
    public class Consultorio
    {
        [Key]
        public int IdConsultorio { get; set; }
        [Display(Name ="Número da Sala")]
        public int NumSala { get; set; }
        [Display(Name ="Característica da Sala")]
        public string? Caracteristica { get; set; }
        [Display(Name ="Unidade:")]
        public int UnidadeId { get; set; }
        public Unidade? Unidade { get; set; }
    }
}
