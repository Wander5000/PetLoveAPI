using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLoveAPI.Context;
using PetLoveAPI.DTOs.Usuario;
using PetLoveAPI.Models;

namespace PetLoveAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase    
    {
        private readonly PetLoveApiContext _context;
        public UsuariosController(PetLoveApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> ListarUsuarios()
        {
            return await _context.Usuarios
                .Include(u => u.RolNavigation)
                .Include(u => u.TipoDocumentoNavigation)
                .Select(u => new UsuarioDTO
                {
                    IdUsuario = u.IdUsuario,
                    NombreUsuario = u.NombreUsuario,
                    TipoDocumento = u.TipoDocumentoNavigation.NombreTipoDoc,
                    NumeroDocumento = u.NumeroDocumento,
                    Correo = u.Correo,
                    Password = u.Password, // Por mientras se corrije el DTO para no exponer la contraseña
                    Direccion = u.Direccion,
                    Rol = u.RolNavigation.NombreRol,
                    Estado = u.Estado
                }) .ToListAsync();

        }

        [HttpPost]
        public async Task<ActionResult<CrearUsuarioDTO>> CrearUsuario(CrearUsuarioDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = new Usuario
            {
                NombreUsuario = dto.NombreUsuario,
                TipoDocumento = dto.TipoDocumento,
                NumeroDocumento = dto.NumeroDocumento,
                Correo = dto.Correo,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Direccion = dto.Direccion,
                Rol = dto.Rol,
                Estado = dto.Estado
            };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarUsuarios), new { id = usuario.IdUsuario }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarUsuario(int id, ActualizarUsuarioDTO dto)
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
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("cambiar-estado/{id}")]
        public async Task<ActionResult> CambiarEstadoUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("El Usuario Solicitado es Erroneo o Inexistente");
            }
            if (usuario.Estado)
            {
                usuario.Estado = false;
            }
            else
            {
                usuario.Estado = true;
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> EliminarUsuario(int id)
        //{
        //    var usuario = await _context.Usuarios.FindAsync(id);
        //    if (usuario == null)
        //    {
        //        return NotFound("El Usuario Solicitado es Erroneo o Inexistente");
        //    }
        //    _context.Usuarios.Remove(usuario);
        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}
    }
}
