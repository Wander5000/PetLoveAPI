using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PetLove.Server.Models;

namespace PetLove.Server.Context;

public partial class PetLoveContext : DbContext
{
    public PetLoveContext()
    {
    }

    public PetLoveContext(DbContextOptions<PetLoveContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriaProducto> CategoriaProductos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<DetallesCompra> DetallesCompras { get; set; }

    public virtual DbSet<DetallesVenta> DetallesVentas { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Imagen> Imagenes { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Medida> Medidas { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<PermisoRol> PermisoRols { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedores { get; set; }

    public virtual DbSet<Rol> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
        //    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("Server=WANDY\\SQLEXPRESS;Initial Catalog=PetLove;integrated security=True;TrustServerCertificate= True");
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriaProducto>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A101ADF9EAA");

            entity.ToTable("CategoriaProducto");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Clientes__D5946642F1E811A6");

            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Municipio).IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroDocumento).IsUnicode(false);
            entity.Property(e => e.TipoDocumento).IsUnicode(false);

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.Usuario)
                .HasConstraintName("FK_Clientes_Usuario");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PK__Compras__0A5CDB5CEDBF81B1");

            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.ProveedorNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.Proveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Compras_Proveedor");
        });

        modelBuilder.Entity<DetallesCompra>(entity =>
        {
            entity.HasKey(e => e.IdDetallesCompras).HasName("PK__Detalles__7DAE565312FC006C");

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
            entity.HasKey(e => e.IdDetallesVenta).HasName("PK__Detalles__8AE2DAABF0D7C49A");

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
            entity.HasKey(e => e.IdEstado).HasName("PK__Estados__FBB0EDC11BF7180C");

            entity.Property(e => e.Nombre).IsUnicode(false);
        });

        modelBuilder.Entity<Imagen>(entity =>
        {
            entity.HasKey(e => e.IdImagen).HasName("PK__Imagenes__B42D8F2A1E33C173");

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
            entity.HasKey(e => e.IdMarca).HasName("PK__Marcas__4076A887A3806394");

            entity.Property(e => e.Nombre).IsUnicode(false);
        });

        modelBuilder.Entity<Medida>(entity =>
        {
            entity.HasKey(e => e.IdMedida).HasName("PK__Medidas__C326EE7D7EB7B537");

            entity.Property(e => e.Nombre).IsUnicode(false);
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("PK__Permisos__0D626EC81941B29C");

            entity.Property(e => e.Descripcion).IsUnicode(false);
        });

        modelBuilder.Entity<PermisoRol>(entity =>
        {
            entity.HasKey(e => e.IdPermisoRol).HasName("PK__PermisoR__E02D6F630DE5DFA6");

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
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__09889210E81D5BAD");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
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
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__E8B631AF054B2138");

            entity.Property(e => e.Celular).IsUnicode(false);
            entity.Property(e => e.Ciudad).IsUnicode(false);
            entity.Property(e => e.Contacto).IsUnicode(false);
            entity.Property(e => e.Correo).IsUnicode(false);
            entity.Property(e => e.Direccion).IsUnicode(false);
            entity.Property(e => e.Documento).IsUnicode(false);
            entity.Property(e => e.Empresa)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre).IsUnicode(false);
            entity.Property(e => e.TipoDocumento).IsUnicode(false);
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roles__2A49584CAE8ADC2B");

            entity.Property(e => e.Descripcion).IsUnicode(false);
            entity.Property(e => e.NombreRol).IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__5B65BF97FDB3632A");

            entity.Property(e => e.Contraseña).IsUnicode(false);
            entity.Property(e => e.Correo).IsUnicode(false);
            entity.Property(e => e.NombreUsuario).IsUnicode(false);

            entity.HasOne(d => d.RolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Rol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Rol");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Ventas__BC1240BD9511035B");

            entity.Property(e => e.MetodoPago).IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.ClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.Cliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Cliente");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.Estado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Estado");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
