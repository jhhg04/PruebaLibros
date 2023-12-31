﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PruebaLibros.Infraestructura.Data;

#nullable disable

namespace PruebaLibros.Infraestructura.Data.MigracionesEF
{
    [DbContext(typeof(ContextoBD))]
    [Migration("20231029013327_Inicial")]
    partial class Inicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PruebaLibros.Dominio.Principal.Entidades.Autor", b =>
                {
                    b.Property<int>("IdAutorPk")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAutorPk"));

                    b.Property<string>("Ciudad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("date");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar");

                    b.HasKey("IdAutorPk");

                    b.ToTable("Autor");
                });

            modelBuilder.Entity("PruebaLibros.Dominio.Principal.Entidades.Libro", b =>
                {
                    b.Property<int>("IdLibroPk")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdLibroPk"));

                    b.Property<int>("Año")
                        .HasColumnType("int");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar");

                    b.Property<int>("IdAutorFk")
                        .HasColumnType("int");

                    b.Property<int>("NumPaginas")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar");

                    b.HasKey("IdLibroPk");

                    b.HasIndex("IdAutorFk");

                    b.ToTable("Libro");
                });

            modelBuilder.Entity("PruebaLibros.Dominio.Principal.Entidades.Libro", b =>
                {
                    b.HasOne("PruebaLibros.Dominio.Principal.Entidades.Autor", "Autor")
                        .WithMany()
                        .HasForeignKey("IdAutorFk")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Autor");
                });
#pragma warning restore 612, 618
        }
    }
}
