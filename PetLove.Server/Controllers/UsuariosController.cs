using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLove.Server.Context;
using PetLove.Server.Dtos.Usuarios;
using PetLove.Server.Models;

namespace PetLove.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly PetLoveContext _context;

        public UsuariosController(PetLoveContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> ListarUsuarios()
        {
            return await _context.Usuarios
                .Include(r => r.RolNavigation)
                .Select(r => new UsuarioDto
                {
                    IdUsuario = r.IdUsuario,
                    NombreUsuario = r.NombreUsuario,
                    Correo = r.Correo,
                    NombreRol = r.RolNavigation.NombreRol,
                    Estado = r.Estado
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<CrearUsuarioDto>> CrearUsuario(CrearUsuarioDto crearUsuarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = new Usuario
            {
                NombreUsuario = crearUsuarioDto.NombreUsuario,
                Correo = crearUsuarioDto.Correo,
                Contraseña = crearUsuarioDto.Contraseña,
                Rol = crearUsuarioDto.Rol,
                Estado = crearUsuarioDto.Estado
            };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarUsuarios), new { id = usuario.IdUsuario }, crearUsuarioDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarRol(int id, ActualizarUsuarioDto actualizarUsuarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("El Usuario Solicitado es Erroneo o Inexistente");
            }
            usuario.NombreUsuario = actualizarUsuarioDto.NombreUsuario;
            usuario.Correo = actualizarUsuarioDto.Correo;
            usuario.Rol = actualizarUsuarioDto.Rol;
            usuario.Estado = actualizarUsuarioDto.Estado;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("El Usuario Solicitado es Erroneo o Inexistente");
            }
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
