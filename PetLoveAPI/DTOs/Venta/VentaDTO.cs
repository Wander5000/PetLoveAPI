namespace PetLoveAPI.DTOs.Venta
{
    public class VentaDTO
    {
        public int IdVenta { get; set; }

        public string Usuario { get; set; } = null!;

        public DateOnly Fecha { get; set; }

        public string MetodoPago { get; set; } = null!;

        public int Descuento { get; set; }

        public decimal Total { get; set; }

        public string? Observaciones { get; set; }

        public int Estado { get; set; }

        public List<DetalleVentaDTO> Detalles { get; set; } = new List<DetalleVentaDTO>();
    }

    public class DetalleVentaDTO
    {
        public int IdDetallesVenta { get; set; }

        public int Venta { get; set; }

        public string Producto { get; set; } = null!;

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal { get; set; }
    }
}
