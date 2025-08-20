using System;
using System.Collections.Generic;

namespace PetLoveAPI.Models;

public partial class DetallesCompra
{
    public int IdDetallesCompras { get; set; }

    public int Compra { get; set; }

    public int Producto { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Subtotal { get; set; }

    public virtual Compra CompraNavigation { get; set; } = null!;

    public virtual Producto ProductoNavigation { get; set; } = null!;
}
