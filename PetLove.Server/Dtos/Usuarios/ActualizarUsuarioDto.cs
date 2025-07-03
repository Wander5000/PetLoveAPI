namespace PetLove.Server.Dtos.Usuarios
{
    public class ActualizarUsuarioDto
    {
        public string NombreUsuario { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public int Rol { get; set; }

        public bool Estado { get; set; }
    }
}
