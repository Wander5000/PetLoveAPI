namespace PetLove.Server.Dtos.Clientes
{
    public class CrearClienteDto
    {
        public long? Usuario { get; set; }

        public string TipoDocumento { get; set; } = null!;

        public string NumeroDocumento { get; set; } = null!;

        public string Nombres { get; set; } = null!;

        public string Apellidos { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Celular { get; set; } = null!;

        public string Municipio { get; set; } = null!;

        public string Direccion { get; set; } = null!;
    }
}
