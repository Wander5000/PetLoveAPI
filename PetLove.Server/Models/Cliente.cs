using System;
using System.Collections.Generic;

namespace PetLove.Server.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public int? Usuario { get; set; }

    public string TipoDocumento { get; set; } = null!;

    public string NumeroDocumento { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Celular { get; set; } = null!;

    public string Municipio { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual Usuario? UsuarioNavigation { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
