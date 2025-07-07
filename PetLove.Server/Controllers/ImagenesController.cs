using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLove.Server.Context;
using PetLove.Server.Dtos.Imagenes;
using PetLove.Server.Dtos.Productos;
using PetLove.Server.Models;

namespace PetLove.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImagenesController : ControllerBase
    {
        private readonly PetLoveContext _context;

        public ImagenesController(PetLoveContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImagenDto>>> ListarImagenes()
        {
            return await _context.Imagenes
                .Include(i => i.ProductoNavigation)
                .Select(i => new ImagenDto
                {
                    IdImagen = i.IdImagen,
                    Url = i.Url,
                    Producto = i.ProductoNavigation.Nombre
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AccionesImagenDto>> CrearImagen(AccionesImagenDto crearImagenDto)
        {

            var imagen = new Imagen
            {
                Url = crearImagenDto.Url,
                Producto = crearImagenDto.Producto
            };
            _context.Imagenes.Add(imagen);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarImagenes), new { id = imagen.IdImagen }, crearImagenDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarImagen(int id, AccionesImagenDto actualizarImagenDto)
        {
            var imagen = await _context.Imagenes.FindAsync(id);
            if (imagen == null)
            {
                return NotFound("Producto no encontrado.");
            }
            imagen.Url = actualizarImagenDto.Url;
            imagen.Producto = actualizarImagenDto.Producto;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarImagen(int id)
        {
            var imagen = await _context.Imagenes.FindAsync(id);
            if (imagen == null)
            {
                return NotFound("Producto no encontrado.");
            }

            _context.Imagenes.Remove(imagen);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
