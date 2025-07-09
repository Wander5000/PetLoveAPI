namespace PetLove.Server.Dtos.Compras
{
    public class CrearCompraDto
    {
        public int Proveedor { get; set; }

        public DateOnly FechaRegistro { get; set; }

        public DateOnly FechaCompra { get; set; }

        public bool Estado { get; set; }

        public List<CrearDetallesCompraDto> Detalles { get; set; } = new List<CrearDetallesCompraDto>();
    }

    public class CrearDetallesCompraDto
    {

        public int Producto { get; set; }

        public int Cantidad { get; set; }
    }
}
