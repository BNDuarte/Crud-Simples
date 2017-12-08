using Crud.Domain.Consultas;
using Crud.Domain.Especialidades;
using Crud.Domain.Medicos;
using Crud.Domain.Pacientes;
using Microsoft.EntityFrameworkCore;

namespace Crud.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Medico> Medico { get; set; }
        public DbSet<Consulta> Consulta { get; set; }
        public DbSet<Especialidade> Especialidade { get; set; }

    }
}
