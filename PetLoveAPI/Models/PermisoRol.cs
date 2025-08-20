using System;
using System.Collections.Generic;

namespace PetLoveAPI.Models;

public partial class PermisoRol
{
    public int IdPermisoRol { get; set; }

    public int Permiso { get; set; }

    public int Rol { get; set; }

    public virtual Permiso PermisoNavigation { get; set; } = null!;

    public virtual Rol RolNavigation { get; set; } = null!;
}
