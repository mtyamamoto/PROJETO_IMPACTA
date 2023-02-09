using System.ComponentModel.DataAnnotations;

namespace AgendaOnline.Models
{
    public class Unidade
    {
        [Key]
        public int UnidadeId { get; set; }
        [Display(Name = "Nome da Unidade:")]
        public string? NomeUnidade { get; set; }
        [Display(Name = "Endereço:")]
        public string? Endereco { get; set; }
        [Display(Name = "Cidade:")]
        public string? Cidade { get; set; }
        [Display(Name ="CEP:")]
        public string? Cep { get; set; }
        [Display(Name = "Telefone:")]
        public string? TelefoneUnid { get; set; }
        [Display(Name = "E-mail:")]
        public string? EmailUnid { get; set; }
        public ICollection<Consultorio> Consultorios { get; set; }
    }
}
