namespace PetLove.Server.Dtos.Clientes
{
    public class ClienteDto
    {
        public int IdCliente { get; set; }

        public string NumeroDocumento { get; set; } = null!;

        public string Nombres { get; set; } = null!;

        public string Apellidos { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Celular { get; set; } = null!;

    }
}
