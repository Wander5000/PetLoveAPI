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
                    Estado = p.Estado
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
            return CreatedAtAction(nameof(ListarProductos), new { id = producto.IdProducto }, dto);
        }
    }
}
