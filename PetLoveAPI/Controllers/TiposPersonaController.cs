using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLoveAPI.Context;
using PetLoveAPI.DTOs.TipoPersona;
using PetLoveAPI.Models;


namespace PetLoveAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TiposPersonaController : ControllerBase
    {
        private readonly PetLoveApiContext _context;

        public TiposPersonaController(PetLoveApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoPersonaDTO>>> ListarTipoPersona()
        {
            return await _context.TipoPersonas
                .Select(tipopersona => new TipoPersonaDTO
                {
                    IdTipoPersona = tipopersona.IdTipoPersona,
                    Nombre = tipopersona.Nombre,
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AccionesTipoPersonaDTO>> CrearTipoPersona(AccionesTipoPersonaDTO dto)
        {
            var tipopersona = new TipoPersona
            {
                Nombre = dto.Nombre,
            };
            _context.TipoPersonas.Add(tipopersona);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarTipoPersona), new { id = tipopersona.IdTipoPersona }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarTipoPersona(int id, AccionesTipoPersonaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tipopersona = await _context.TipoPersonas.FindAsync(id);
            if (tipopersona == null)
            {
                return NotFound("El TipoPersona Solicitado es Erroneo o Inexistente");
            }
            tipopersona.Nombre = dto.Nombre;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarTipoPersona(int id)
        {
            var tipopersona = await _context.TipoPersonas.FindAsync(id);
            if (tipopersona == null)
            {
                return NotFound("El TipoPersona Solicitado es Erroneo o Inexistente");
            }
            _context.TipoPersonas.Remove(tipopersona);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
