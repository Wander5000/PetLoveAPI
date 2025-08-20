using System;
using System.Collections.Generic;

namespace PetLoveAPI.Models;

public partial class TipoDocumento
{
    public int IdTipoDocumento { get; set; }

    public string NombreTipoDoc { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
