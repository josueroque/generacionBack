using GeneracionAPI.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneracionAPI.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Fuente> Fuentes { get; set; }
        public DbSet<Zona> Zonas { get; set; }
        public DbSet<Nivel> Niveles{ get; set; }
        public DbSet<Origen> Origenes { get; set; }
        public DbSet<Planta> Plantas { get; set; }
        public DbSet<Subestacion> Subestaciones { get; set; }
        public DbSet<Tension> Tensiones{ get; set; }
        public DbSet<Archivo> Archivos { get; set; }

        public DbSet<ScadaValor> ScadaValores { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Planta>()
             .HasIndex(p => new { p.Nombre })
            .IsUnique(true);

            modelBuilder.Entity<Subestacion>()
                 .HasIndex(p => new { p.Nombre })
                .IsUnique(true);

            modelBuilder.Entity<Archivo>()
                .HasIndex(p => new { p.Fecha, p.SCADA })
                .IsUnique(true);

            modelBuilder.Entity<ScadaValor>()
                .HasIndex(p => new { p.Fecha, p.Hora,p.PlantaId })
                .IsUnique(true);
        }
    }
}