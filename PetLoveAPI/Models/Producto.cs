using System;
using System.Collections.Generic;

namespace PetLoveAPI.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public int Categoria { get; set; }

    public int Stock { get; set; }

    public int Cantidad { get; set; }

    public int Medida { get; set; }

    public int Marca { get; set; }

    public decimal Precio { get; set; }

    public bool Estado { get; set; }

    public virtual CategoriaProducto CategoriaNavigation { get; set; } = null!;

    public virtual ICollection<DetallesCompra> DetallesCompras { get; set; } = new List<DetallesCompra>();

    public virtual ICollection<DetallesVenta> DetallesVenta { get; set; } = new List<DetallesVenta>();

    public virtual ICollection<Imagen> Imagenes { get; set; } = new List<Imagen>();

    public virtual Marca MarcaNavigation { get; set; } = null!;

    public virtual Medida MedidaNavigation { get; set; } = null!;
}
