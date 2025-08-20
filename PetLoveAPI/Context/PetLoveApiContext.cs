using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PetLoveAPI.Models;

namespace PetLoveAPI.Context;

public partial class PetLoveApiContext : DbContext
{
    public PetLoveApiContext()
    {
    }

    public PetLoveApiContext(DbContextOptions<PetLoveApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriaProducto> CategoriaProductos { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<DetallesCompra> DetallesCompras { get; set; }

    public virtual DbSet<DetallesVenta> DetallesVentas { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Imagen> Imagenes { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Medida> Medidas { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<PermisoRol> PermisoRols { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedores { get; set; }

    public virtual DbSet<Rol> Roles { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
        //    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("Server= WANDY\\SQLEXPRESS;Initial Catalog=PetLoveAPI;integrated security=True;TrustServerCertificate=True");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriaProducto>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A10ADCE2A44");

            entity.ToTable("CategoriaProducto");

            entity.Property(e => e.Descripcion).IsUnicode(false);
            entity.Property(e => e.NombreCategoria).IsUnicode(false);
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PK__Compras__0A5CDB5CD481FB5C");

            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.ProveedorNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.Proveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Compras_Proveedor");
        });

        modelBuilder.Entity<DetallesCompra>(entity =>
        {
            entity.HasKey(e => e.IdDetallesCompras).HasName("PK__Detalles__7DAE56533E3C9615");

            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CompraNavigation).WithMany(p => p.DetallesCompras)
                .HasForeignKey(d => d.Compra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallesCompras_Compra");

            entity.HasOne(d => d.ProductoNavigation).WithMany(p => p.DetallesCompras)
                .HasForeignKey(d => d.Producto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallesCompras_Producto");
        });

        modelBuilder.Entity<DetallesVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetallesVenta).HasName("PK__Detalles__8AE2DAAB903801DD");

            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.ProductoNavigation).WithMany(p => p.DetallesVenta)
                .HasForeignKey(d => d.Producto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallesVentas_Producto");

            entity.HasOne(d => d.VentaNavigation).WithMany(p => p.DetallesVenta)
                .HasForeignKey(d => d.Venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallesVentas_Venta");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__Estados__FBB0EDC148593FED");

            entity.Property(e => e.NombreEstado).IsUnicode(false);
        });

        modelBuilder.Entity<Imagen>(entity =>
        {
            entity.HasKey(e => e.IdImagen).HasName("PK__Imagenes__B42D8F2AD92701DB");

            entity.Property(e => e.Url)
                .IsUnicode(false)
                .HasColumnName("URL");

            entity.HasOne(d => d.ProductoNavigation).WithMany(p => p.Imagenes)
                .HasForeignKey(d => d.Producto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Imagenes_Producto");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__Marcas__4076A887B1C088FA");

            entity.Property(e => e.NombreMarca).IsUnicode(false);
        });

        modelBuilder.Entity<Medida>(entity =>
        {
            entity.HasKey(e => e.IdMedida).HasName("PK__Medidas__C326EE7DC1DE8671");

            entity.Property(e => e.NombreMedida).IsUnicode(false);
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("PK__MetodoPa__6F49A9BEA8874A0D");

            entity.ToTable("MetodoPago");

            entity.Property(e => e.NombreMetodoPago).IsUnicode(false);
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("PK__Permisos__0D626EC87145524E");

            entity.Property(e => e.Descripcion).IsUnicode(false);
        });

        modelBuilder.Entity<PermisoRol>(entity =>
        {
            entity.HasKey(e => e.IdPermisoRol).HasName("PK__PermisoR__E02D6F63BBF83968");

            entity.ToTable("PermisoRol");

            entity.HasOne(d => d.PermisoNavigation).WithMany(p => p.PermisoRols)
                .HasForeignKey(d => d.Permiso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PermisoRol_Permiso");

            entity.HasOne(d => d.RolNavigation).WithMany(p => p.PermisoRols)
                .HasForeignKey(d => d.Rol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PermisoRol_Rol");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__09889210F5223A3F");

            entity.Property(e => e.NombreProducto).IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Categoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Categoria");

            entity.HasOne(d => d.MarcaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Marca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Marca");

            entity.HasOne(d => d.MedidaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Medida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Medida");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__E8B631AF7F716B2D");

            entity.Property(e => e.Celular).IsUnicode(false);
            entity.Property(e => e.Ciudad).IsUnicode(false);
            entity.Property(e => e.Contacto).IsUnicode(false);
            entity.Property(e => e.Correo).IsUnicode(false);
            entity.Property(e => e.Direccion).IsUnicode(false);
            entity.Property(e => e.Documento).IsUnicode(false);
            entity.Property(e => e.Empresa).IsUnicode(false);
            entity.Property(e => e.Nombre).IsUnicode(false);
            entity.Property(e => e.TipoDocumento).IsUnicode(false);
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roles__2A49584C34647B32");

            entity.Property(e => e.Descripcion).IsUnicode(false);
            entity.Property(e => e.NombreRol).IsUnicode(false);
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumento).HasName("PK__TipoDocu__3AB3332FC6AD3B65");

            entity.ToTable("TipoDocumento");

            entity.Property(e => e.NombreTipoDoc).IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__5B65BF975541A7B2");

            entity.Property(e => e.Correo).IsUnicode(false);
            entity.Property(e => e.Direccion).IsUnicode(false);
            entity.Property(e => e.NombreUsuario).IsUnicode(false);
            entity.Property(e => e.NumeroDocumento).IsUnicode(false);
            entity.Property(e => e.Password).IsUnicode(false);

            entity.HasOne(d => d.RolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Rol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Rol");

            entity.HasOne(d => d.TipoDocumentoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_TipoDocumento");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Ventas__BC1240BDBFB01F59");

            entity.Property(e => e.Observaciones).IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.Estado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Estado");

            entity.HasOne(d => d.MetodoPagoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.MetodoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_MetodoPago");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.Usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
