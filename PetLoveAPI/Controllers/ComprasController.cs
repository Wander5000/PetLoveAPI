using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLoveAPI.Context;
using PetLoveAPI.Models;
using PetLoveAPI.DTOs.Compra;

namespace PetLoveAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly PetLoveApiContext _context;

        public ComprasController(PetLoveApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompraDTO>>> ListarCompras()
        {
            var compras = await _context.Compras
                .Include(c => c.ProveedorNavigation)
                .Include(c => c.DetallesCompras).ThenInclude(dc => dc.ProductoNavigation)
                .ToListAsync();

            var resultado = compras.Select(c => new CompraDTO
            {
                IdCompra = c.IdCompra,
                Proveedor = c.ProveedorNavigation.Nombre,
                FechaRegistro = c.FechaRegistro,
                FechaCompra = c.FechaCompra,
                Total = c.Total,
                Estado = c.Estado,
                Detalles = c.DetallesCompras.Select(dc => new DetalleCompraDTO
                {
                    IdDetallesCompras = dc.IdDetallesCompras,
                    Compra = dc.Compra,
                    Producto = dc.ProductoNavigation.NombreProducto,
                    Cantidad = dc.Cantidad,
                    PrecioUnitario = dc.PrecioUnitario,
                    Subtotal = dc.Subtotal
                }).ToList()
            }).ToList();
            return Ok(resultado);
        }

        [HttpPost]
        public async Task <ActionResult<CrearCompraDTO>> CrearCompra(CrearCompraDTO dto)
        {
            try
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                var compra = new Compra
                {
                    Proveedor = dto.Proveedor,
                    FechaRegistro = DateOnly.FromDateTime(DateTime.Now),
                    FechaCompra = dto.FechaCompra,
                    Total = dto.Total,
                    Estado = dto.Estado
                };
                _context.Compras.Add(compra);
                await _context.SaveChangesAsync();
                foreach (var detalleDto in dto.Detalles)
                {
                    var detalleCompra = new DetallesCompra
                    {
                        Compra = compra.IdCompra,
                        Producto = detalleDto.Producto,
                        Cantidad = detalleDto.Cantidad,
                    };
                    _context.DetallesCompras.Add(detalleCompra);
                    var producto = await _context.Productos.FindAsync(detalleDto.Producto);
                    if (producto != null)
                    {
                        producto.Stock += detalleDto.Cantidad;
                        _context.Productos.Update(producto);
                    }
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return CreatedAtAction(nameof(ListarCompras), new { id = compra.IdCompra }, dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
