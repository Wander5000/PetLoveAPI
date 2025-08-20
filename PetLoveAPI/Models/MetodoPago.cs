using System;
using System.Collections.Generic;

namespace PetLoveAPI.Models;

public partial class MetodoPago
{
    public int IdMetodoPago { get; set; }

    public string NombreMetodoPago { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
