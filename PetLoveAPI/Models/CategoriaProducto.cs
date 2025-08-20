using System;
using System.Collections.Generic;

namespace PetLoveAPI.Models;

public partial class CategoriaProducto
{
    public int IdCategoria { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
