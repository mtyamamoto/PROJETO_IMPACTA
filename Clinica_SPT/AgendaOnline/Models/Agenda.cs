using System.ComponentModel.DataAnnotations;

namespace AgendaOnline.Models
{
    public class Agenda
    {
        [Key]
        public int AgendaId { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public int HorarioId { get; set; }
        public Horario Horario { get; set; }
    }
}
