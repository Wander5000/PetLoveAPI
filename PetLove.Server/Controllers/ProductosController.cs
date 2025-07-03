using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLove.Server.Context;
using PetLove.Server.Dtos.Productos;
using PetLove.Server.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetLove.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly PetLoveContext _context;

        public ProductosController(PetLoveContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> ListarProductos()
        {
            return await _context.Productos
                .Include(p => p.CategoriaNavigation)
                .Select(p => new ProductoDto
                {
                    IdProducto = p.IdProducto,
                    Nombre = p.Nombre,
                    Categoria = p.CategoriaNavigation.Nombre,
                    Stock = p.Stock,
                    Estado = p.Estado
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AccionesProductoDto>> CrearProducto(AccionesProductoDto crearProductoDto)
        {

            var producto = new Producto
            {
                Nombre = crearProductoDto.NombreProducto,
                Categoria = crearProductoDto.Categoria,
                Stock = crearProductoDto.Stock,
                Medida = crearProductoDto.Medida,
                Cantidad = crearProductoDto.Cantidad,
                Marca = crearProductoDto.Marca,
                Precio = crearProductoDto.Precio,
                Estado = crearProductoDto.Estado

            };
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarProductos), new { id = producto.IdProducto }, crearProductoDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, AccionesProductoDto ActualizarProductoDto)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }

            producto.Nombre = ActualizarProductoDto.NombreProducto;
            producto.Categoria = ActualizarProductoDto.Categoria;
            producto.Stock = ActualizarProductoDto.Stock;
            producto.Medida = ActualizarProductoDto.Medida;
            producto.Cantidad = ActualizarProductoDto.Cantidad;
            producto.Marca = ActualizarProductoDto.Marca;
            producto.Precio = ActualizarProductoDto.Precio;
            producto.Estado = ActualizarProductoDto.Estado;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}