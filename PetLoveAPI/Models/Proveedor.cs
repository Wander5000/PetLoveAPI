using System;
using System.Collections.Generic;

namespace PetLoveAPI.Models;

public partial class Proveedor
{
    public int IdProveedor { get; set; }

    public int TipoDocumento { get; set; }

    public string Documento { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Celular { get; set; } = null!;

    public string? Telefono { get; set; }

    public string Direccion { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual TipoDocumento TipoDocumentoNavigation { get; set; } = null!;
}
