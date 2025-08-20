namespace PetLoveAPI.DTOs.Usuario
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        public string NombreUsuario { get; set; } = null!;

        public int TipoDocumento { get; set; }

        public string NumeroDocumento { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string Rol { get; set; } = null!;

        public bool Estado { get; set; }
    }
}
