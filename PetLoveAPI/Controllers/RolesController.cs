using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLoveAPI.Context;
using PetLoveAPI.DTOs.Rol;
using PetLoveAPI.Models;

namespace PetLoveAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly PetLoveApiContext _context;
        public RolesController(PetLoveApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolDTO>>> ListarRoles()
        {
            return await _context.Roles
                .Select(rol => new RolDTO
                {
                    IdRol = rol.IdRol,
                    NombreRol = rol.NombreRol,
                    Descripcion = rol.Descripcion
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AccionesRolDTO>> CrearRol(AccionesRolDTO dto)
        {
            var rol = new Rol
            {
                NombreRol = dto.NombreRol,
                Descripcion = dto.Descripcion
            };
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarRoles), new { id = rol.IdRol }, dto);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult> ActualizarRol(int id, CrearRolDto rolDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var rol = await _context.Roles.FindAsync(id);
        //    if (rol == null)
        //    {
        //        return NotFound("El Rol Solicitado es Erroneo o Inexistente");
        //    }
        //    rol.NombreRol = rolDto.NombreRol;
        //    rol.Descripcion = rolDto.Descripcion;
        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> EliminarRol(int id)
        //{
        //    var rol = await _context.Roles.FindAsync(id);
        //    if (rol == null)
        //    {
        //        return NotFound("El Rol Solicitado es Erroneo o Inexistente");
        //    }
        //    _context.Roles.Remove(rol);
        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}
    }
}
