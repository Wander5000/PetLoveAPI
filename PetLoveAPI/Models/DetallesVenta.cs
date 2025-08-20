using System;
using System.Collections.Generic;

namespace PetLoveAPI.Models;

public partial class DetallesVenta
{
    public int IdDetallesVenta { get; set; }

    public int Venta { get; set; }

    public int Producto { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Subtotal { get; set; }

    public virtual Producto ProductoNavigation { get; set; } = null!;

    public virtual Venta VentaNavigation { get; set; } = null!;
}
