using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AgendaOnline.Models
{
    public class Horario
    {
        [Key]
        public int HorarioId { get; set; }


        [Display(Name = "Horário")]
        public DateTime HorarioConsulta { get; set; }

        public int ConsultorioId { get; set; }
        [Display(Name ="No Consultorio:")]
        public Consultorio Consultorio { get; set; }
        [Display(Name = "Com o Especialista:")]
        public int EspecialistaId { get; set; }
        public Especialista Especialistas { get; set; }
    }
}
