namespace PetLoveAPI.DTOs.Auth
{
    public class RegisterDTO
    {
        public string NombreUsuario { get; set; } = null!;

        public int TipoDocumento { get; set; }

        public string NumeroDocumento { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Direccion { get; set; } = null!;

    }
}
