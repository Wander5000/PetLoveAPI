using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLoveAPI.Context;
using PetLoveAPI.DTOs.TipoDocumento;
using PetLoveAPI.Models;

namespace PetLoveAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TiposDocumentoController : ControllerBase
    {
        private readonly PetLoveApiContext _context;
        public TiposDocumentoController(PetLoveApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoDocumentoDTO>>> ListarTiposDoc()
        {
            return await _context.TipoDocumentos
                .Select(td => new TipoDocumentoDTO
                {
                    IdTipoDocumento = td.IdTipoDocumento,
                    NombreTipoDoc = td.NombreTipoDoc,
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AccionesTipoDocumentoDTO>> CrearTipoDoc(AccionesTipoDocumentoDTO dto)
        {
            var td = new TipoDocumento
            {
                NombreTipoDoc = dto.NombreTipoDoc,
            };
            _context.TipoDocumentos.Add(td);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarTiposDoc), new { id = td.IdTipoDocumento }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarTipoDoc(int id, AccionesTipoDocumentoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tipodocumento = await _context.TipoDocumentos.FindAsync(id);
            if (tipodocumento == null)
            {
                return NotFound("El Tipo de Documento Solicitado es Erroneo o Inexistente");
            }
            tipodocumento.NombreTipoDoc = dto.NombreTipoDoc;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarTipoDoc(int id)
        {
            var tipodocumento = await _context.TipoDocumentos.FindAsync(id);
            if (tipodocumento == null)
            {
                return NotFound("El Tipo de Documento Solicitado es Erroneo o Inexistente");
            }
            _context.TipoDocumentos.Remove(tipodocumento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
