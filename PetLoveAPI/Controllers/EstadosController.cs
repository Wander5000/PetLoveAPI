using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLoveAPI.Context;
using PetLoveAPI.Models;
using PetLoveAPI.DTOs.Estado;

namespace PetLoveAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EstadosController : ControllerBase
    {
        private readonly PetLoveApiContext _context;
        public EstadosController(PetLoveApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccionesEstadoDTO>>> ListarEstados()
        {
            return await _context.Estados
                .Select(estado => new AccionesEstadoDTO
                {
                    NombreEstado = estado.NombreEstado
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AccionesEstadoDTO>> CrearEstado(AccionesEstadoDTO dto)
        {
            var estado = new Estado
            {
                NombreEstado = dto.NombreEstado
            };
            _context.Estados.Add(estado);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarEstados), new { id = estado.IdEstado }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarEstado(int id, AccionesEstadoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var estado = await _context.Estados.FindAsync(id);
            if (estado == null)
            {
                return NotFound("El Estado Solicitado es Erroneo o Inexistente");
            }
            estado.NombreEstado = dto.NombreEstado;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarEstado(int id)
        {
            var estado = await _context.Estados.FindAsync(id);
            if (estado == null)
            {
                return NotFound("El Estado Solicitado es Erroneo o Inexistente");
            }
            _context.Estados.Remove(estado);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
