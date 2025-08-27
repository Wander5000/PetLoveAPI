namespace PetLoveAPI.DTOs.CategoriaProducto
{
    public class CategoriaProductoDTO
    {
        public int IdCategoria { get; set; }

        public string NombreCategoria { get; set; } = null!;

        public string Descripcion { get; set; } = null!;
    }
}
