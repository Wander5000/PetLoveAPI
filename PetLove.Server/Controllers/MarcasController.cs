using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLove.Server.Context;
using PetLove.Server.Dtos.Marcas;
using PetLove.Server.Models;

namespace PetLove.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        private readonly PetLoveContext _context;

        public MarcasController(PetLoveContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcaDto>>> ListarMarcas()
        {
            return await _context.Marcas
                .Select(m => new MarcaDto
                {
                    IdMarca = m.IdMarca,
                    NombreMarca = m.Nombre
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<MarcaDto>> CrearMarca(AccionesMarcaDto crearMarcaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var marca = new Marca
            {
                Nombre = crearMarcaDto.NombreMarca,
            };

            _context.Marcas.Add(marca);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarMarcas), new { id = marca.IdMarca }, crearMarcaDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarMarca(int id, AccionesMarcaDto actualizarMarcaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null)
            {
                return NotFound("Marca no encontrada.");
            }
            marca.Nombre = actualizarMarcaDto.NombreMarca;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarMarca(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null)
            {
                return NotFound("Marca no encontrada.");
            }

            _context.Marcas.Remove(marca);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}