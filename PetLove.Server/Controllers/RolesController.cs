using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetLove.Server.Context;
using PetLove.Server.Models;
using PetLove.Server.Dtos.Roles;

namespace PetLove.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly PetLoveContext _context;

        public RolesController(PetLoveContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolDto>>> ListarRoles()
        {
            return await _context.Roles.
                Select(r => new RolDto
                {
                    IdRol = r.IdRol,
                    NombreRol = r.NombreRol,
                    Descripcion = r.Descripcion
                }).ToListAsync();
        }

        [HttpPost]
        public async  Task<ActionResult<CrearRolDto>> CrearRol(CrearRolDto crearRolDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rol = new Rol
            {
                NombreRol = crearRolDto.NombreRol,
                Descripcion = crearRolDto.Descripcion
            };
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarRoles), new { id = rol.IdRol }, crearRolDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarRol(int id, CrearRolDto rolDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
            {
                return NotFound("El Rol Solicitado es Erroneo o Inexistente");
            }
            rol.NombreRol = rolDto.NombreRol;
            rol.Descripcion = rolDto.Descripcion;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarRol(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
            {
                return NotFound("El Rol Solicitado es Erroneo o Inexistente");
            }
            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
