using System;
using System.Collections.Generic;

namespace PetLoveAPI.Models;

public partial class Medida
{
    public int IdMedida { get; set; }

    public string NombreMedida { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
