namespace PetLove.Server.Dtos.Ventas
{
    public class VentaDto
    {
        public int IdVenta { get; set; }

        public string Cliente { get; set; } = null!;

        public DateOnly Fecha { get; set; }

        public string MetodoPago { get; set; } = null!;

        public decimal Total { get; set; }

        public string Estado { get; set; } = null!;

        public List<DetallesVentaDto> Detalles { get; set; } = new List<DetallesVentaDto>();
    }

    public class DetallesVentaDto
    {
        public int IdDetallesVenta { get; set; }

        public string Producto { get; set; } = null!;

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal { get; set; }
    }
}
