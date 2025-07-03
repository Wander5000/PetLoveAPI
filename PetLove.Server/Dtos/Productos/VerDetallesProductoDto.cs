namespace PetLove.Server.Dtos.Productos
{
    public class VerDetallesProductoDto
    {
        public int IdProducto { get; set; }

        public string Nombre { get; set; } = null!;

        public string Categoria { get; set; } = null!;

        public int Stock { get; set; }

        public int Cantidad { get; set; }

        public string Medida { get; set; } = null!;

        public string Marca { get; set; } = null!;

        public decimal Precio { get; set; }

        public bool Estado { get; set; }
    }
}
