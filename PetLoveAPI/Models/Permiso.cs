using System;
using System.Collections.Generic;

namespace PetLoveAPI.Models;

public partial class Permiso
{
    public int IdPermiso { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<PermisoRol> PermisoRols { get; set; } = new List<PermisoRol>();
}
