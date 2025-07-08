namespace PetLove.Server.Dtos.Ventas
{
    public class CrearVentaDto
    {
        public int Cliente { get; set; }

        public DateOnly Fecha { get; set; }

        public string MetodoPago { get; set; } = null!;

        public int Estado { get; set; }

        public List<CrearDetallesVentaDto> Detalles { get; set; } = new List<CrearDetallesVentaDto>();
    }

    public class CrearDetallesVentaDto
    { 
        public int Producto { get; set; }

        public int Cantidad { get; set; }
    }
}
