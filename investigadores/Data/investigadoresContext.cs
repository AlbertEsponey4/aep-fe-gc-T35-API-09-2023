using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using investigadores.Models;

namespace investigadores.Data;

public partial class investigadoresContext : DbContext
{
    public investigadoresContext(DbContextOptions<investigadoresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<Facultad> Facultads { get; set; }

    public virtual DbSet<Investigadore> Investigadores { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.NumSerie).HasName("PRIMARY");

            entity.ToTable("equipos");

            entity.HasIndex(e => e.Facultad, "facultad");

            entity.Property(e => e.NumSerie)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("numSerie");
            entity.Property(e => e.Facultad).HasColumnName("facultad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasOne(d => d.FacultadNavigation).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.Facultad)
                .HasConstraintName("equipos_ibfk_1");
        });

        modelBuilder.Entity<Facultad>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("facultad");

            entity.Property(e => e.Codigo)
                .ValueGeneratedNever()
                .HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Investigadore>(entity =>
        {
            entity.HasKey(e => e.Dni).HasName("PRIMARY");

            entity.ToTable("investigadores");

            entity.HasIndex(e => e.Facultad, "facultad");

            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .HasColumnName("DNI");
            entity.Property(e => e.Facultad).HasColumnName("facultad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");

            entity.HasOne(d => d.FacultadNavigation).WithMany(p => p.Investigadores)
                .HasForeignKey(d => d.Facultad)
                .HasConstraintName("investigadores_ibfk_1");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => new { e.Dni, e.NumSerie })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("reserva");

            entity.HasIndex(e => e.NumSerie, "numSerie");

            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .HasColumnName("DNI");
            entity.Property(e => e.NumSerie)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("numSerie");
            entity.Property(e => e.Comienzo).HasColumnName("comienzo");
            entity.Property(e => e.Fin).HasColumnName("fin");

            entity.HasOne(d => d.DniNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.Dni)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reserva_ibfk_1");

            entity.HasOne(d => d.NumSerieNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.NumSerie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reserva_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
