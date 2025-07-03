namespace PetLove.Server.Dtos.Productos
{
    public class AccionesProductoDto
    {
        public string NombreProducto { get; set; } = null!;

        public int Categoria { get; set; }

        public int Stock { get; set; }

        public int Medida { get; set; }

        public int Cantidad { get; set; }

        public int Marca { get; set; }

        public decimal Precio { get; set; }

        public bool Estado { get; set; }
    }
}
