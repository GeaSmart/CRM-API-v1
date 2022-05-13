using CRM.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Configurando PK Compuesta AgenteProspecto
            modelBuilder.Entity<AgenteProspecto>().HasKey(x => new { x.AgenteId, x.ProspectoId });
        }

        public DbSet<Agente> Agentes { get; set; }
        public DbSet<Prospecto> Prospectos { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<AgenteProspecto> AgentesProspectos { get; set; }
    }
}
