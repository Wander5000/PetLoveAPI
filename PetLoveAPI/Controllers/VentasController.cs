using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLoveAPI.Context;
using PetLoveAPI.DTOs.Venta;
using PetLoveAPI.Models;

namespace PetLoveAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly PetLoveApiContext _context;
        public VentasController(PetLoveApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VentaDTO>>> ListarVentas()
        {
            var ventas = await _context.Ventas
                .Include(v => v.UsuarioNavigation)
                .Include(v => v.MetodoPagoNavigation)
                .Include(v => v.EstadoNavigation)
                .Include(v => v.DetallesVenta).ThenInclude(dv => dv.ProductoNavigation)
                .ToListAsync();

            var resultado = ventas.Select(v => new VentaDTO
            {
                IdVenta = v.IdVenta,
                Usuario = v.UsuarioNavigation.NombreUsuario,
                Fecha = v.Fecha,
                MetodoPago = v.MetodoPagoNavigation.NombreMetodoPago,
                Descuento = v.Descuento,
                Total = v.Total,
                Observaciones = v.Observaciones,
                Estado = v.Estado,
                Detalles = v.DetallesVenta.Select(dv => new DetalleVentaDTO
                {
                    IdDetallesVenta = dv.IdDetallesVenta,
                    Venta = dv.Venta,
                    Producto = dv.ProductoNavigation.NombreProducto,
                    Cantidad = dv.Cantidad,
                    PrecioUnitario = dv.PrecioUnitario,
                    Subtotal = dv.Subtotal
                }).ToList()
            }).ToList();
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<CrearVentaDTO>> CrearVenta(CrearVentaDTO dto)
        {
            try
            {
                using var transaction = await _context.Database.BeginTransactionAsync();

                var venta = new Venta
                {
                    Usuario = dto.Usuario,
                    Descuento = dto.Descuento,
                    Estado = dto.Estado,
                    Fecha = dto.Fecha,
                    MetodoPago = dto.MetodoPago,
                };
                _context.Ventas.Add(venta);
                await _context.SaveChangesAsync();

                decimal totalVenta = 0;
                foreach(var detalleDto in dto.Detalles)
                {
                    var producto = await _context.Productos.FindAsync(detalleDto.Producto);
                    if (producto == null)
                    {
                        return BadRequest($"Producto con ID {detalleDto.Producto} no encontrado.");
                    }
                    var nuevoStock = producto.Stock - detalleDto.Cantidad;
                    if (nuevoStock < 0)
                    {
                        return BadRequest($"No hay suficiente stock para el producto {producto.NombreProducto}.");
                    }
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
                venta.Total = totalVenta - (totalVenta * (dto.Descuento / 100));
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return CreatedAtAction(nameof(CrearVenta), new { id = venta.IdVenta }, dto);
            }
            catch
            {
                return BadRequest("Error al crear la venta.");
            }
        }
    }
}
