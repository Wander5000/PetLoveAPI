using System;
using System.Collections.Generic;

namespace PetLove.Server.Models;

public partial class Imagene
{
    public int IdImagen { get; set; }

    public string Url { get; set; } = null!;

    public int Producto { get; set; }

    public virtual Producto ProductoNavigation { get; set; } = null!;
}
