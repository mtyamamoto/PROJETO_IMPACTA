using System.ComponentModel.DataAnnotations;

namespace AgendaOnline.Models
{
    public class PagamentoConsulta
    {
        [Key]
        public int IdPagamento { get; set; }
        public int EspecialistaId { get; set; }
        public Especialista Especialista { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public Decimal Valor { get; set; }
    }
}
