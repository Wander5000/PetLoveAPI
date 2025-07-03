namespace PetLove.Server.Dtos.CategoriasProducto
{
    public class CategoriaProductoDto
    {
        public int IdCategoria { get; set; }

        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;
    }
}
