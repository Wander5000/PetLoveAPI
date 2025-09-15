using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLoveAPI.Context;
using PetLoveAPI.Models;
using PetLoveAPI.DTOs.MetodoPago;


namespace PetLoveAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MetodosPagoController : ControllerBase
    {
        private readonly PetLoveApiContext _context;
        public MetodosPagoController(PetLoveApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MetodoPagoDTO>>> ListarMetodosPago()
        {
            return await _context.MetodoPago
                .Select(metodo => new MetodoPagoDTO
                {
                    IdMetodoPago = metodo.IdMetodoPago,
                    NombreMetodoPago = metodo.NombreMetodoPago
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<MetodoPagoDTO>> CrearMetodoPago(MetodoPagoDTO dto)
        {
            var metodoPago = new MetodoPago
            {
                NombreMetodoPago = dto.NombreMetodoPago
            };
            _context.MetodoPago.Add(metodoPago);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarMetodosPago), new { id = metodoPago.IdMetodoPago }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarMetodoPago(int id, AccionesMetodoPagoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var metodoPago = await _context.MetodoPago.FindAsync(id);
            if (metodoPago == null)
            {
                return NotFound("El Metodo de Pago Solicitado es Erroneo o Inexistente");
            }
            metodoPago.NombreMetodoPago = dto.NombreMetodoPago;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarMetodoPago(int id)
        {
            var metodoPago = await _context.MetodoPago.FindAsync(id);
            if (metodoPago == null)
            {
                return NotFound("El Metodo de Pago Solicitado es Erroneo o Inexistente");
            }
            _context.MetodoPago.Remove(metodoPago);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
