﻿// <auto-generated />
using System;
using GeneracionAPI.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GeneracionAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GeneracionAPI.Entidades.Archivo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ruta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SCADA")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Fecha", "SCADA")
                        .IsUnique();

                    b.ToTable("Archivos");
                });

            modelBuilder.Entity("GeneracionAPI.Entidades.Fuente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Fuentes");
                });

            modelBuilder.Entity("GeneracionAPI.Entidades.Nivel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.HasKey("Id");

                    b.ToTable("Niveles");
                });

            modelBuilder.Entity("GeneracionAPI.Entidades.Origen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Origenes");
                });

            modelBuilder.Entity("GeneracionAPI.Entidades.Planta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FuenteId")
                        .HasColumnType("int");

                    b.Property<int>("Nodo")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Nomenclatura")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrigenId")
                        .HasColumnType("int");

                    b.Property<string>("RotulacionSCADA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubestacionId")
                        .HasColumnType("int");

                    b.Property<int>("TensionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FuenteId");

                    b.HasIndex("OrigenId");

                    b.HasIndex("SubestacionId");

                    b.HasIndex("TensionId");

                    b.ToTable("Plantas");
                });

            modelBuilder.Entity("GeneracionAPI.Entidades.ScadaValor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("Hora")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuarioGuarda")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuarioModifica")
                        .HasColumnType("int");

                    b.Property<float>("Valor")
                        .HasColumnType("real");

                    b.Property<DateTime>("fechaHoraGuarda")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("fechaHoraModifica")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Fecha", "Hora")
                        .IsUnique();

                    b.ToTable("ScadaValores");
                });

            modelBuilder.Entity("GeneracionAPI.Entidades.Subestacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("Nomenclatura")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ZonaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ZonaId");

                    b.ToTable("Subestaciones");
                });

            modelBuilder.Entity("GeneracionAPI.Entidades.Tension", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("NivelId")
                        .HasColumnType("int");

                    b.Property<float>("tension")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("NivelId");

                    b.ToTable("Tensiones");
                });

            modelBuilder.Entity("GeneracionAPI.Entidades.Zona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.ToTable("Zonas");
                });

            modelBuilder.Entity("GeneracionAPI.Entidades.Planta", b =>
                {
                    b.HasOne("GeneracionAPI.Entidades.Subestacion", "Fuente")
                        .WithMany()
                        .HasForeignKey("FuenteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GeneracionAPI.Entidades.Origen", "Origen")
                        .WithMany()
                        .HasForeignKey("OrigenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GeneracionAPI.Entidades.Subestacion", "Subestacion")
                        .WithMany()
                        .HasForeignKey("SubestacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GeneracionAPI.Entidades.Tension", "Tension")
                        .WithMany()
                        .HasForeignKey("TensionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GeneracionAPI.Entidades.Subestacion", b =>
                {
                    b.HasOne("GeneracionAPI.Entidades.Zona", "Zona")
                        .WithMany()
                        .HasForeignKey("ZonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GeneracionAPI.Entidades.Tension", b =>
                {
                    b.HasOne("GeneracionAPI.Entidades.Nivel", "Nivel")
                        .WithMany()
                        .HasForeignKey("NivelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
