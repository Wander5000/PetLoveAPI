using System;
using System.Collections.Generic;

namespace PetLove.Server.Models;

public partial class Venta
{
    public int IdVenta { get; set; }

    public int Cliente { get; set; }

    public DateOnly Fecha { get; set; }

    public string MetodoPago { get; set; } = null!;

    public decimal Total { get; set; }

    public int Estado { get; set; }

    public virtual Cliente ClienteNavigation { get; set; } = null!;

    public virtual ICollection<DetallesVenta> DetallesVenta { get; set; } = new List<DetallesVenta>();

    public virtual Estado EstadoNavigation { get; set; } = null!;
}
