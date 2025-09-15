using System;
using System.Collections.Generic;

namespace PetLoveAPI.Models;

public partial class PasswordRecovery
{
    public int Id { get; set; }

    public int Usuario { get; set; }

    public string Token { get; set; } = null!;

    public DateTime Creado { get; set; }

    public DateTime Vence { get; set; }

    public bool Usado { get; set; }

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
