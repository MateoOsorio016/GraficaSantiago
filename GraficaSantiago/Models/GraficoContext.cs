using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GraficaSantiago.Models;

public partial class GraficoContext : DbContext
{
    public GraficoContext()
    {
    }

    public GraficoContext(DbContextOptions<GraficoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DetalleVentum> DetalleVenta { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            //                optionsBuilder.UseSqlServer("/*Server=MEDAPRCSGFSP542\\SQLEXPRESS;Initial Catalog=TallerCrud;integrated security=True; TrustServerCertificate=True*/");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DetalleVentum>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK__DetalleV__BFE2843FAF28DED5");

            entity.Property(e => e.IdDetalleVenta).HasColumnName("idDetalleVenta");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreProducto");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__DetalleVe__idVen__4CA06362");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Venta__077D56144477CBDF");

            entity.Property(e => e.IdVenta)
                .ValueGeneratedNever()
                .HasColumnName("idVenta");
            entity.Property(e => e.DocumentoCliente)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("documentoCliente");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.ImpuestoTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("impuestoTotal");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("nombreCliente");
            entity.Property(e => e.NumeroVenta)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("numeroVenta");
            entity.Property(e => e.SubTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("subTotal");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
