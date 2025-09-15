using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLoveAPI.Context;
using PetLoveAPI.Models;
using PetLoveAPI.DTOs.Producto;

namespace PetLoveAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly PetLoveApiContext _context;

        public ProductosController(PetLoveApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> ListarProductos()
        {
            return await _context.Productos
                .Include(p => p.CategoriaNavigation)
                .Include(p => p.MarcaNavigation)
                .Include(p => p.MedidaNavigation)
                .Include(p => p.Imagenes)
                .Select(p => new ProductoDTO
                {
                    IdProducto = p.IdProducto,
                    NombreProducto = p.NombreProducto,
                    Categoria = p.CategoriaNavigation.NombreCategoria,
                    Stock = p.Stock,
                    Cantidad = p.Cantidad,
                    Medida = p.MedidaNavigation.NombreMedida,
                    Marca = p.MarcaNavigation.NombreMarca,
                    Precio = p.Precio,
                    Estado = p.Estado,
                    Imagenes = p.Imagenes.Select(i => new ImagenDTO
                    {
                        IdImagen = i.IdImagen,
                        Url = i.Url
                    }).ToList()
                })
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AccionesProductoDTO>> CrearProducto(AccionesProductoDTO dto)
        {
            var producto = new Producto
            {
                NombreProducto = dto.NombreProducto,
                Categoria = dto.Categoria,
                Stock = dto.Stock,
                Cantidad = dto.Cantidad,
                Medida = dto.Medida,
                Marca = dto.Marca,
                Precio = dto.Precio,
                Estado = dto.Estado
            };
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            if(dto.Imagenes != null && dto.Imagenes.Count > 0)
            {
                foreach(var imagenDto in dto.Imagenes)
                {
                    var imagen = new Imagen
                    {
                        Producto = producto.IdProducto,
                        Url = imagenDto.Url
                    };
                    _context.Imagenes.Add(imagen);
                }
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction(nameof(ListarProductos), new { id = producto.IdProducto }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, AccionesProductoDTO dto)
        {
            var producto = await _context.Productos
                .Include(p => p.Imagenes)
                .FirstOrDefaultAsync(p => p.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }
            producto.NombreProducto = dto.NombreProducto;
            producto.Categoria = dto.Categoria;
            producto.Stock = dto.Stock;
            producto.Cantidad = dto.Cantidad;
            producto.Medida = dto.Medida;
            producto.Marca = dto.Marca;
            producto.Precio = dto.Precio;
            producto.Estado = dto.Estado;
            // Actualizar imágenes
            if (dto.Imagenes != null)
            {
                // Eliminar imágenes existentes
                _context.Imagenes.RemoveRange(producto.Imagenes);
                // Agregar nuevas imágenes
                foreach (var imagenDto in dto.Imagenes)
                {
                    var imagen = new Imagen
                    {
                        Producto = producto.IdProducto,
                        Url = imagenDto.Url
                    };
                    _context.Imagenes.Add(imagen);
                }
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.Imagenes)
                .FirstOrDefaultAsync(p => p.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Imagenes.RemoveRange(producto.Imagenes);
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("cambiar-estado/{id}")]
        public async Task<IActionResult> ActualizarEstadoProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            if (producto.Estado)
            {
                producto.Estado = false;
            }
            else
            {
                producto.Estado = true;
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
