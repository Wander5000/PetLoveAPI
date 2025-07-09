using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLove.Server.Context;
using PetLove.Server.Dtos.Compras;
using PetLove.Server.Models;

namespace PetLove.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly PetLoveContext _context;

        public ComprasController(PetLoveContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompraDto>>> ListarCompras()
        {
            var compras = await _context.Compras
                .Include(c => c.ProveedorNavigation)
                .Include(c => c.DetallesCompras).ThenInclude(d => d.ProductoNavigation)
                .ToListAsync();

            var resultado = compras.Select(c => new CompraDto
            {
                IdCompra = c.IdCompra,
                NombreProveedor = c.ProveedorNavigation.Nombre,
                FechaRegistro = c.FechaRegistro,
                FechaCompra = c.FechaCompra,
                Total = c.Total,
                Estado = c.Estado,
                Detalles = c.DetallesCompras.Select(d => new DetalleCompraDto
                {
                    IdDetalle = d.IdDetallesCompras,
                    Producto = d.ProductoNavigation.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                }).ToList()
            }).ToList();

            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<CrearCompraDto>> CrearCompra(CrearCompraDto crearCompraDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using var transaccion = await _context.Database.BeginTransactionAsync();

            try
            {
                var compra = new Compra
                {
                    Proveedor = crearCompraDto.Proveedor,
                    FechaRegistro = crearCompraDto.FechaRegistro,
                    FechaCompra = crearCompraDto.FechaCompra,
                    Estado = crearCompraDto.Estado
                };
                _context.Compras.Add(compra);
                await _context.SaveChangesAsync();

                decimal totalCompra = 0;

                foreach (var detalleDto in crearCompraDto.Detalles)
                {
                    var producto = await _context.Productos.FindAsync(detalleDto.Producto);
                    if (producto == null)
                    {
                        throw new Exception($"Producto con ID {detalleDto.Producto} no encontrado.");
                    }
                    var precioUnitario = producto.Precio;
                    var subtotal = precioUnitario * detalleDto.Cantidad;
                    var stock = producto.Stock + detalleDto.Cantidad;
                    totalCompra += subtotal;

                    producto.Stock = stock;

                    var detalle = new DetallesCompra
                    {
                        Compra = compra.IdCompra,
                        Producto = detalleDto.Producto,
                        Cantidad = detalleDto.Cantidad,
                        PrecioUnitario = precioUnitario,
                        Subtotal = subtotal
                    };
                    producto.Stock += detalleDto.Cantidad; 
                    _context.Productos.Update(producto);
                    _context.DetallesCompras.Add(detalle);
                }
                compra.Total = totalCompra;
                await _context.SaveChangesAsync();
                await transaccion.CommitAsync();
                return CreatedAtAction(nameof(ListarCompras), new { id = compra.IdCompra }, crearCompraDto);
            }
            catch
            {
                await transaccion.RollbackAsync();
                return BadRequest("Error al crear la compra. Por favor, intente nuevamente.");
            }
        }
    }
}
