namespace PetLoveAPI.DTOs.Venta
{
    public class CrearVentaDTO
    {
        public int Usuario { get; set; } 

        public DateOnly Fecha { get; set; }

        public int MetodoPago { get; set; }

        public int Descuento { get; set; }

        public string? Observaciones { get; set; }

        public int Estado { get; set; }

        public List<CrearDetalleVentaDTO> Detalles { get; set; } = new List<CrearDetalleVentaDTO>();
    }

    public class CrearDetalleVentaDTO
    {
        public int Producto { get; set; }

        public int Cantidad { get; set; }
    }
}
