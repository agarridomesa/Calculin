﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using dbContext;

namespace ContextoCore.Migrations
{
    [DbContext(typeof(CalculinDB))]
    partial class CalculinDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Modelo.Conversión", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Destinosiglas");

                    b.Property<double>("Factorconversion");

                    b.Property<string>("Origensiglas");

                    b.HasKey("Id");

                    b.ToTable("Conversiones");
                });

            modelBuilder.Entity("Modelo.Historial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cantidad");

                    b.Property<double>("FactorConversion");

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("NickUsuario");

                    b.Property<double>("Resutado");

                    b.Property<string>("SMD");

                    b.Property<string>("SMO");

                    b.HasKey("Id");

                    b.ToTable("Historiales");
                });

            modelBuilder.Entity("Modelo.Moneda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("EnUso");

                    b.Property<string>("Nombre");

                    b.Property<string>("Siglas");

                    b.HasKey("Id");

                    b.ToTable("Monedas");
                });

            modelBuilder.Entity("Modelo.Pais", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre");

                    b.Property<string>("Siglas");

                    b.HasKey("Id");

                    b.ToTable("Paises");
                });

            modelBuilder.Entity("Modelo.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<DateTime>("FechaNacimiento");

                    b.Property<int>("IdHistorial");

                    b.Property<int>("IdPais");

                    b.Property<string>("Nick");

                    b.Property<string>("Nombre");

                    b.Property<string>("Pass");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}