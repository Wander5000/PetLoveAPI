using PetLoveAPI.Context;
using PetLoveAPI.Models;
using PetLoveAPI.DTOs.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetLoveAPI.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly PetLoveApiContext _context;
        private readonly IConfiguration _config;

        public AuthService(PetLoveApiContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        private string GenerateToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                expires: DateTime.Now.AddHours(2)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ResponseDTO> LoginAsync(LoginDTO request)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == request.Correo);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Password, usuario.Password))
            {
                throw new UnauthorizedAccessException("Correo o Contraseña Incorrectos");
            }

            return new ResponseDTO
            {
                NombreUsuario = usuario.NombreUsuario,
                Token = GenerateToken(usuario)
            };
        }

        public async Task<ResponseDTO> RegisterAsync(RegisterDTO request)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Correo == request.Correo))
            {
                throw new InvalidOperationException("El correo ya está en uso");
            }
            var nuevoUsuario = new Usuario
            {
                NombreUsuario = request.NombreUsuario,
                TipoDocumento = request.TipoDocumento,
                NumeroDocumento = request.NumeroDocumento,
                Correo = request.Correo,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Direccion = request.Direccion,
                Rol = 1,
                Estado = true
            };
            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();
            return new ResponseDTO
            {
                NombreUsuario = nuevoUsuario.NombreUsuario,
                Token = GenerateToken(nuevoUsuario)
            };
        }
    }
}
