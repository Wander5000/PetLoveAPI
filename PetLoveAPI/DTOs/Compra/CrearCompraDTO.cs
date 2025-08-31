namespace PetLoveAPI.DTOs.Compra
{
    public class CrearCompraDTO
    {
        public int Proveedor { get; set; }

        public DateOnly FechaCompra { get; set; }

        public decimal Total { get; set; }

        public bool Estado { get; set; }

        public List<CrearDetalleCompraDTO> Detalles { get; set; } = new List<CrearDetalleCompraDTO>();
    }

    public class CrearDetalleCompraDTO
    {
        public int Producto { get; set; }

        public int Cantidad { get; set; }
    }

}
