using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLove.Server.Context;
using PetLove.Server.Dtos.Estados;
using PetLove.Server.Models;

namespace PetLove.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EstadosController : ControllerBase
    {
        private readonly PetLoveContext _context;

        public EstadosController(PetLoveContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoDto>>> ListarEstados()
        {
            return await _context.Estados.
                Select(e => new EstadoDto
                {
                    IdEstado = e.IdEstado,
                    Nombre = e.Nombre
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AccionesEstadoDto>> CrearEstado(AccionesEstadoDto crearEstadoDto)
        {
            var estado = new Estado
            {
                Nombre = crearEstadoDto.Nombre
            };
            _context.Estados.Add(estado);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarEstados), new { id = estado.IdEstado }, crearEstadoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarEstado(int id, AccionesEstadoDto actualizarEstadoDto)
        {
            var estado = await _context.Estados.FindAsync(id);
            if (estado == null)
            {
                return NotFound("El Estado Solicitado es Erroneo o Inexistente");
            }
            estado.Nombre = actualizarEstadoDto.Nombre;
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
