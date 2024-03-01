using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiAlmacen.Models
{
    public partial class FacturaCamiloContext : DbContext
    {
        public FacturaCamiloContext()
        {
        }

        public FacturaCamiloContext(DbContextOptions<FacturaCamiloContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Factura> Facturas { get; set; } = null!;
        public virtual DbSet<FacturaDetail> FacturaDetails { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Vendedor> Vendedors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
      
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Identificacion)
                    .HasName("PK_Cliente_1");

                entity.ToTable("Cliente");

                entity.Property(e => e.Identificacion).ValueGeneratedNever();

                entity.Property(e => e.Correo).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.IdFactura);

                entity.ToTable("Factura");

                entity.Property(e => e.CcCliente).HasColumnName("CC_Cliente");

                entity.Property(e => e.CcVendedor).HasColumnName("CC_Vendedor");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.CcClienteNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.CcCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Factura_Cliente");

                entity.HasOne(d => d.CcVendedorNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.CcVendedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Factura_Vendedor");
            });

            modelBuilder.Entity<FacturaDetail>(entity =>
            {
                entity.HasKey(e => e.IdDetalle);

                entity.ToTable("FacturaDetail");

                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.FacturaDetails)
                    .HasForeignKey(d => d.IdFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FacturaDetail_Factura");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.FacturaDetails)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Factura_Producto");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.ToTable("Producto");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Precio).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Vendedor>(entity =>
            {
                entity.HasKey(e => e.Identificacion)
                    .HasName("PK_Vendedor_1");

                entity.ToTable("Vendedor");

                entity.Property(e => e.Identificacion).ValueGeneratedNever();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
