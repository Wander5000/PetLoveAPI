using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLove.Server.Context;
using PetLove.Server.Dtos.Medidas;
using PetLove.Server.Models;

namespace PetLove.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MedidasController : ControllerBase
    {
        private readonly PetLoveContext _context;

        public MedidasController(PetLoveContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedidaDto>>> ListarMedidas()
        {
            return await _context.Medidas
                .Select(m => new MedidaDto
                {
                    IdMedida = m.IdMedida,
                    Nombre = m.Nombre,
                }).ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult<MedidaDto>> CrearMedida(AccionesMedidaDto medidaDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Hubo un Error Con Tu solicitud, Revisa que los campos esten correctos y vuelve a intentarlo");
            }
            var medida = new Medida
            {
                Nombre = medidaDto.NombreMedida
            };

            _context.Medidas.Add(medida);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarMedidas), new { id = medida.IdMedida }, medidaDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarMedida(int id, AccionesMedidaDto medidaDto)
        {
            var medida = await _context.Medidas.FindAsync(id);
            if (medida == null)
            {
                return NotFound("Medida no encontrada.");
            }
            medida.Nombre = medidaDto.NombreMedida;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarMedida(int id)
        {
            var medida = await _context.Medidas.FindAsync(id);
            if (medida == null)
            {
                return NotFound("Medida no encontrada.");
            }
            _context.Medidas.Remove(medida);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}