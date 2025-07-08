namespace PetLove.Server.Dtos.Imagenes
{
    public class ImagenDto
    {
        public int IdImagen { get; set; }

        public string Url { get; set; } = null!;

        public string Producto { get; set; } = null!;
    }
}
