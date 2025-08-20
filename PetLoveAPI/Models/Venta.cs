﻿using System;
using System.Collections.Generic;

namespace PetLoveAPI.Models;

public partial class Venta
{
    public int IdVenta { get; set; }

    public int Usuario { get; set; }

    public DateOnly Fecha { get; set; }

    public int MetodoPago { get; set; }

    public int Descuento { get; set; }

    public decimal Total { get; set; }

    public string? Observaciones { get; set; }

    public int Estado { get; set; }

    public virtual ICollection<DetallesVenta> DetallesVenta { get; set; } = new List<DetallesVenta>();

    public virtual Estado EstadoNavigation { get; set; } = null!;

    public virtual MetodoPago MetodoPagoNavigation { get; set; } = null!;

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
