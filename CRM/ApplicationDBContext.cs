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
        public DbSet<Agente> Agentes { get; set; }
        public DbSet<Prospecto> Prospectos { get; set; }
    }
}
