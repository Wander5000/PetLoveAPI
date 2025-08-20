using System;
using System.Collections.Generic;

namespace PetLoveAPI.Models;

public partial class Compra
{
    public int IdCompra { get; set; }

    public int Proveedor { get; set; }

    public DateOnly FechaRegistro { get; set; }

    public DateOnly FechaCompra { get; set; }

    public decimal Total { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<DetallesCompra> DetallesCompras { get; set; } = new List<DetallesCompra>();

    public virtual Proveedor ProveedorNavigation { get; set; } = null!;
}
