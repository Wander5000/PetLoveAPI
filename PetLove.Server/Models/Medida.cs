using System;
using System.Collections.Generic;

namespace PetLove.Server.Models;

public partial class Medida
{
    public int IdMedida { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
