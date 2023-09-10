using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using grandesAlmacenes.Models;

namespace grandesAlmacenes.Data;

public partial class grandesAlmacenesContext : DbContext
{
    public grandesAlmacenesContext(DbContextOptions<grandesAlmacenesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cajero> Cajeros { get; set; }

    public virtual DbSet<MaquinasRegistradora> MaquinasRegistradoras { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Cajero>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("cajeros");

            entity.Property(e => e.Codigo)
                .ValueGeneratedNever()
                .HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<MaquinasRegistradora>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("maquinas_registradoras");

            entity.Property(e => e.Codigo)
                .ValueGeneratedNever()
                .HasColumnName("codigo");
            entity.Property(e => e.Piso).HasColumnName("piso");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("productos");

            entity.Property(e => e.Codigo)
                .ValueGeneratedNever()
                .HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio).HasColumnName("precio");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => new { e.Cajero, e.Maquina, e.Producto })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("venta");

            entity.HasIndex(e => e.Maquina, "maquina");

            entity.HasIndex(e => e.Producto, "producto");

            entity.Property(e => e.Cajero).HasColumnName("cajero");
            entity.Property(e => e.Maquina).HasColumnName("maquina");
            entity.Property(e => e.Producto).HasColumnName("producto");

            entity.HasOne(d => d.CajeroNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.Cajero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("venta_ibfk_1");

            entity.HasOne(d => d.MaquinaNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.Maquina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("venta_ibfk_2");

            entity.HasOne(d => d.ProductoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.Producto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("venta_ibfk_3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
