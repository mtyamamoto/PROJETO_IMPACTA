using System.ComponentModel.DataAnnotations;

namespace AgendaOnline.Models
{
    public class Paciente
    {
        [Key]
        public int PacienteId { get; set; }
        [Display(Name ="Nome do Paciente:")]
        public string? NomePaciente { get; set; }
        [Display(Name = "E-mail:")]
        public string? Email { get; set; }
        [Display(Name = "Telefone de Contato:")]
        public string? Telefone1 { get; set; }
        [Display(Name = "Celular:")]
        public string? Celular { get; set; }
        [Display(Name = "Observação:")]
        public string? Observacao { get; set; }

        public ICollection<Agenda> Agendas { get; set; }
    }
}
