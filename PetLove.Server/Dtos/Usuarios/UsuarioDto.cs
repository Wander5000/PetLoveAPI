namespace PetLove.Server.Dtos.Usuarios
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }

        public string NombreUsuario { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string NombreRol { get; set; }

        public bool Estado { get; set; }
    }
}
