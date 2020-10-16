using GeneracionAPI.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GeneracionAPI.Contexts
{
    public class ApplicationDbContext :IdentityDbContext
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
        public DbSet<ComercialDato> ComercialDatos{ get; set; }
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
            SeedData(modelBuilder);

            modelBuilder.Entity<ComercialDato>()
                .HasIndex(p => new { p.Fecha, p.Hora, p.PlantaId })
                .IsUnique(true);
            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
        private void SeedData(ModelBuilder modelBuilder)
        {

            var rolAdminId = "4ba49888-bc69-4994-9c0b-a5d2b35e12a3";
            var usuarioAdminId = "d18bcb24-fe36-43e7-a340-d3dfe8c2566d";

            var rolAdmin = new IdentityRole()
            {
                Id = rolAdminId,
                Name = "Admin",
                NormalizedName = "Admin"
            };

            var passwordHasher = new PasswordHasher<IdentityUser>();

            var username = "jroque@ods.org.hn";

            var usuarioAdmin = new IdentityUser()
            {
                Id = usuarioAdminId,
                UserName = username,
                NormalizedUserName = username,
                Email = username,
                NormalizedEmail = username,
                PasswordHash = passwordHasher.HashPassword(null, "Java2015")
            };

            //modelBuilder.Entity<IdentityUser>()
            //    .HasData(usuarioAdmin);

            //modelBuilder.Entity<IdentityRole>()
            //    .HasData(rolAdmin);

            //modelBuilder.Entity<IdentityUserClaim<string>>()
            //    .HasData(new IdentityUserClaim<string>()
            //    {
            //        Id = 1,
            //        ClaimType = ClaimTypes.Role,
            //        UserId = usuarioAdminId,
            //        ClaimValue = "Admin"
            //    });
        }
        }
    }