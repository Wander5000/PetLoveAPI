using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLoveAPI.Context;
using PetLoveAPI.Models;
using PetLoveAPI.DTOs.Medida;

namespace PetLoveAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedidasController : ControllerBase
    {
        private readonly PetLoveApiContext _context;

        public MedidasController(PetLoveApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedidaDTO>>> ListarMedidas()
        {
            return await _context.Medidas
                .Select(medida => new MedidaDTO
                {
                    IdMedida = medida.IdMedida,
                    NombreMedida = medida.NombreMedida
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<MedidaDTO>> CrearMedida(MedidaDTO dto)
        {
            var medida = new Medida
            {
                NombreMedida = dto.NombreMedida
            };
            _context.Medidas.Add(medida);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarMedidas), new { id = medida.IdMedida }, dto);
        }
    }
}
