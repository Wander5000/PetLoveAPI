using System;
using System.Collections.Generic;

namespace PetLoveAPI.Models;

public partial class Rol
{
    public int IdRol { get; set; }

    public string NombreRol { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<PermisoRol> PermisoRols { get; set; } = new List<PermisoRol>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
