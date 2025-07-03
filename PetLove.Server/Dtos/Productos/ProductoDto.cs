namespace PetLove.Server.Dtos.Productos
{
    public class ProductoDto
    {
        public int IdProducto { get; set; }

        public string Nombre { get; set; } = null!;

        public string Categoria { get; set; } = null!;

        public int Stock { get; set; }

        public bool Estado { get; set; }
    }
}
