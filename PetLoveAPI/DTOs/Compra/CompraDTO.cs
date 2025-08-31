namespace PetLoveAPI.DTOs.Compra
{
    public class CompraDTO
    {
        public int IdCompra { get; set; }

        public string Proveedor { get; set; } = null!;

        public DateOnly FechaRegistro { get; set; }

        public DateOnly FechaCompra { get; set; }

        public decimal Total { get; set; }

        public bool Estado { get; set; }

        public List<DetalleCompraDTO> Detalles { get; set; } = new List<DetalleCompraDTO>();
    }

    public class DetalleCompraDTO
    {
        public int IdDetallesCompras { get; set; }

        public int Compra { get; set; }

        public string Producto { get; set; } = null!;

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal { get; set; }
    }
}
