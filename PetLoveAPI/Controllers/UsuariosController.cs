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
    }
}
