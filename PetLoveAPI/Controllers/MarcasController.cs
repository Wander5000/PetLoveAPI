using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLoveAPI.Context;
using PetLoveAPI.Models;
using PetLoveAPI.DTOs.Marca;

namespace PetLoveAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        private readonly PetLoveApiContext _context;
        public MarcasController(PetLoveApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcaDTO>>> ListarMarcas()
        {
             return await _context.Marcas
                .Select(marca => new MarcaDTO
                {
                    IdMarca = marca.IdMarca,
                    NombreMarca = marca.NombreMarca
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<MarcaDTO>> CrearMarca(MarcaDTO dto)
        {
            var marca = new Marca
            {
                NombreMarca = dto.NombreMarca
            };
            _context.Marcas.Add(marca);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarMarcas), new { id = marca.IdMarca }, dto);
        }
    }
}
