﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReversiMvcApp.Data;

namespace ReversiMvcApp.Migrations
{
    [DbContext(typeof(ReversiDbContext))]
    [Migration("20220311142626_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReversiMvcApp.Models.Speler", b =>
                {
                    b.Property<string>("Guid")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("AantalGelijk")
                        .HasColumnType("int");

                    b.Property<int>("AantalGewonnen")
                        .HasColumnType("int");

                    b.Property<int>("AantalVerloren")
                        .HasColumnType("int");

                    b.Property<string>("Naam")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Guid");

                    b.ToTable("Spelers");
                });
#pragma warning restore 612, 618
        }
    }
}