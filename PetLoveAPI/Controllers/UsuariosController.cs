using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLoveAPI.Context;
using PetLoveAPI.DTOs.Usuario;

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
                .Select(u => new UsuarioDTO
                {
                    IdUsuario = u.IdUsuario,
                    NombreUsuario = u.NombreUsuario,
                    TipoDocumento = u.TipoDocumento,
                    NumeroDocumento = u.NumeroDocumento,
                    Correo = u.Correo,
                    Password = u.Password, // Por mientras se corrije el DTO para no exponer la contraseña
                    Direccion = u.Direccion,
                    Rol = u.RolNavigation.NombreRol,
                    Estado = u.Estado
                }) .ToListAsync();

        }

        //[HttpPost]
        //public async Task<ActionResult<CrearUsuarioDto>> CrearUsuario(CrearUsuarioDto crearUsuarioDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var usuario = new Usuario
        //    {
        //        NombreUsuario = crearUsuarioDto.NombreUsuario,
        //        TipoDocumento = crearUsuarioDto.TipoDocumento,
        //        NumeroDocumento = crearUsuarioDto.NumeroDocumento,
        //        Correo = crearUsuarioDto.Correo,
        //        Password = crearUsuarioDto.Password,
        //        Direccion = crearUsuarioDto.Direccion,
        //        Rol = crearUsuarioDto.Rol,
        //        Estado = crearUsuarioDto.Estado
        //    };
        //    _context.Usuarios.Add(usuario);
        //    await _context.SaveChangesAsync();
        //    return CreatedAtAction(nameof(ListarUsuarios), new { id = usuario.IdUsuario }, crearUsuarioDto);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult> ActualizarUsuario(int id, ActualizarUsuarioDto actualizarUsuarioDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var usuario = await _context.Usuarios.FindAsync(id);
        //    if (usuario == null)
        //    {
        //        return NotFound("El Usuario Solicitado es Erroneo o Inexistente");
        //    }
        //    usuario.NombreUsuario = actualizarUsuarioDto.NombreUsuario;
        //    usuario.TipoDocumento = actualizarUsuarioDto.TipoDocumento;
        //    usuario.NumeroDocumento = actualizarUsuarioDto.NumeroDocumento;
        //    usuario.Correo = actualizarUsuarioDto.Correo;
        //    usuario.Direccion = actualizarUsuarioDto.Direccion;
        //    usuario.Rol = actualizarUsuarioDto.Rol;
        //    usuario.Estado = actualizarUsuarioDto.Estado;
        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}

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
