using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using piezas.Models;

namespace piezas.Data;

public partial class piezasContext : DbContext
{
    public piezasContext(DbContextOptions<piezasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pieza> Piezas { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Suministra> Suministras { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Pieza>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("piezas");

            entity.Property(e => e.Codigo)
                .ValueGeneratedNever()
                .HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("proveedores");

            entity.Property(e => e.Codigo)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Suministra>(entity =>
        {
            entity.HasKey(e => new { e.CodigoPieza, e.IdProveedor })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("suministra");

            entity.HasIndex(e => e.IdProveedor, "idProveedor");

            entity.Property(e => e.CodigoPieza).HasColumnName("codigoPieza");
            entity.Property(e => e.IdProveedor)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("idProveedor");
            entity.Property(e => e.Precio).HasColumnName("precio");

            entity.HasOne(d => d.CodigoPiezaNavigation).WithMany(p => p.Suministras)
                .HasForeignKey(d => d.CodigoPieza)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("suministra_ibfk_1");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Suministras)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("suministra_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
