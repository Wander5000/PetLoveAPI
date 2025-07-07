namespace PetLove.Server.Dtos.Compras
{
    public class CompraDto
    {
        public int IdCompra { get; set; }

        public string NombreProveedor { get; set; } = null!;

        public DateOnly FechaRegistro { get; set; }

        public DateOnly FechaCompra { get; set; }

        public decimal Total { get; set; }

        public bool Estado { get; set; }

        public List<DetalleCompraDto> Detalles { get; set; } = new List<DetalleCompraDto>();
    }

    public class DetalleCompraDto
    {
        public int IdDetalle { get; set; }

        public int Compra { get; set; }

        public string Producto { get; set; } = null!;

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal { get; set; }
    }
}
