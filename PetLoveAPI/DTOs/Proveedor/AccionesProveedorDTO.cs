namespace PetLoveAPI.DTOs.Proveedor
{
    public class AccionesProveedorDTO
    {
        public int TipoDocumento { get; set; }

        public string Documento { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Celular { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string Ciudad { get; set; } = null!;

        public bool Estado { get; set; }

    }
}
