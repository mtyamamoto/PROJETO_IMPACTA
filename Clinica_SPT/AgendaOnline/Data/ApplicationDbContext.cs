using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AgendaOnline.Models;

namespace AgendaOnline.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AgendaOnline.Models.Agenda> Agenda { get; set; }
        public DbSet<AgendaOnline.Models.Consulta> Consulta { get; set; }
        public DbSet<AgendaOnline.Models.Consultorio> Consultorio { get; set; }
        public DbSet<AgendaOnline.Models.Especialidade> Especialidade { get; set; }
        public DbSet<AgendaOnline.Models.Especialista> Especialista { get; set; }
        public DbSet<AgendaOnline.Models.Horario> Horario { get; set; }
        public DbSet<AgendaOnline.Models.Paciente> Paciente { get; set; }
        public DbSet<AgendaOnline.Models.PagamentoConsulta> PagamentoConsulta { get; set; }
        public DbSet<AgendaOnline.Models.Unidade> Unidade { get; set; }
    }
}