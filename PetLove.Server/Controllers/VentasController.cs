using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLove.Server.Context;
using PetLove.Server.Dtos.Ventas;
using PetLove.Server.Models;

namespace PetLove.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly PetLoveContext _context;

        public VentasController(PetLoveContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VentaDto>>> ListarVentas()
        {
            var ventas = await _context.Ventas
                .Include(v => v.ClienteNavigation)
                .Include(v => v.DetallesVenta).ThenInclude(d => d.ProductoNavigation)
                .Include(v => v.EstadoNavigation)
                .ToListAsync();

            var resultado = ventas.Select(v => new VentaDto
            {
                IdVenta = v.IdVenta,
                Cliente = v.ClienteNavigation.Nombres,
                Fecha = v.Fecha,
                MetodoPago = v.MetodoPago,
                Total = v.Total,
                Estado = v.EstadoNavigation.Nombre,
                Detalles = v.DetallesVenta.Select(d => new DetallesVentaDto
                {
                    IdDetallesVenta = d.IdDetallesVenta,
                    Producto = d.ProductoNavigation.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                }).ToList()
            }).ToList();

            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<CrearVentaDto>> CrearVenta(CrearVentaDto crearVentaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using var transaccion = await _context.Database.BeginTransactionAsync();

            try
            {
                var venta = new Venta
                {
                    Cliente = crearVentaDto.Cliente,
                    Fecha = crearVentaDto.Fecha,
                    MetodoPago = crearVentaDto.MetodoPago,
                    Estado = crearVentaDto.Estado
                };
                _context.Ventas.Add(venta);
                await _context.SaveChangesAsync();

                decimal totalVenta = 0;
                foreach (var detalleDto in crearVentaDto.Detalles)
                {
                    var producto = await _context.Productos.FindAsync(detalleDto.Producto);
                    if (producto == null)
                    {
                        return NotFound($"Producto con ID {detalleDto.Producto} no encontrado.");
                    }

                    var nuevoStock = producto.Stock - detalleDto.Cantidad;
                    var precioUnitario = producto.Precio;
                    var subtotal = precioUnitario * detalleDto.Cantidad;
                    totalVenta += subtotal;
                    producto.Stock = nuevoStock;

                    var detalle = new DetallesVenta
                    {
                        Venta = venta.IdVenta,
                        Producto = detalleDto.Producto,
                        Cantidad = detalleDto.Cantidad,
                        PrecioUnitario = precioUnitario,
                        Subtotal = subtotal
                    };
                    _context.DetallesVentas.Add(detalle);
                }
                venta.Total = totalVenta;
                await _context.SaveChangesAsync();
                await transaccion.CommitAsync();
                return CreatedAtAction(nameof(CrearVenta), new { id = venta.IdVenta }, crearVentaDto);
            }
            catch
            {
                await transaccion.RollbackAsync();
                return BadRequest("Error al crear la venta. Por favor, intente nuevamente.");
            }
        }
    }
}
