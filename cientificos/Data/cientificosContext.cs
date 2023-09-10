using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using cientificos.Models;

namespace cientificos.Data;

public partial class cientificosContext : DbContext
{
    public cientificosContext(DbContextOptions<cientificosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cientifico> Cientificos { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Cientifico>(entity =>
        {
            entity.HasKey(e => e.Dni).HasName("PRIMARY");

            entity.ToTable("cientificos");

            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .HasColumnName("DNI");
            entity.Property(e => e.NomApels).HasMaxLength(255);

            entity.HasMany(d => d.Proyectos).WithMany(p => p.Cientificos)
                .UsingEntity<Dictionary<string, object>>(
                    "AsignadoA",
                    r => r.HasOne<Proyecto>().WithMany()
                        .HasForeignKey("Proyecto")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Proyecto_fk"),
                    l => l.HasOne<Cientifico>().WithMany()
                        .HasForeignKey("Cientifico")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Cientifico_fk"),
                    j =>
                    {
                        j.HasKey("Cientifico", "Proyecto")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("asignado_a");
                        j.HasIndex(new[] { "Proyecto" }, "Proyecto_fk");
                        j.IndexerProperty<string>("Cientifico").HasMaxLength(8);
                        j.IndexerProperty<string>("Proyecto")
                            .HasMaxLength(4)
                            .IsFixedLength();
                    });
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proyectos");

            entity.Property(e => e.Id)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
