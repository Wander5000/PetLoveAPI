namespace PetLoveAPI.DTOs.Proveedor
{
    public class ProveedorDTO
    {
        public int IdProveedor { get; set; }

        public string Empresa { get; set; } = null!;

        public string TipoDocumento { get; set; } = null!;

        public string Documento { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Celular { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string Ciudad { get; set; } = null!;

        public bool Estado { get; set; }

        public string? Contacto { get; set; }
    }
}
