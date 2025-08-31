using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLoveAPI.Context;
using PetLoveAPI.Models;
using PetLoveAPI.DTOs.Proveedor;

namespace PetLoveAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly PetLoveApiContext _context;
        public ProveedoresController(PetLoveApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorDTO>>> ListarProveedores()
        {
            var proveedores = await _context.Proveedores
                .Select(p => new ProveedorDTO
                {
                    IdProveedor = p.IdProveedor,
                    Empresa = p.Empresa,
                    TipoDocumento = p.TipoDocumento,
                    Documento = p.Documento,
                    Nombre = p.Nombre,
                    Correo = p.Correo,
                    Celular = p.Celular,
                    Direccion = p.Direccion,
                    Ciudad = p.Ciudad,
                    Estado = p.Estado,
                    Contacto = p.Contacto
                }).ToListAsync();

            return Ok(proveedores);
        }

        [HttpPost]
        public async Task<ActionResult<ProveedorDTO>> CrearProveedor(ProveedorDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var proveedor = new Proveedor
            {
                IdProveedor = dto.IdProveedor,
                Empresa = dto.Empresa,
                TipoDocumento = dto.TipoDocumento,
                Documento = dto.Documento,
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Celular = dto.Celular,
                Direccion = dto.Direccion,
                Ciudad = dto.Ciudad,
                Estado = dto.Estado,
                Contacto = dto.Contacto
            };

            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ListarProveedores), new { id = proveedor.IdProveedor }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarProveedor(int id, AccionesProveedorDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound("El proveedor solicitado no existe.");
            }

            proveedor.Empresa = dto.Empresa;
            proveedor.TipoDocumento = dto.TipoDocumento;
            proveedor.Documento = dto.Documento;
            proveedor.Nombre = dto.Nombre;
            proveedor.Correo = dto.Correo;
            proveedor.Celular = dto.Celular;
            proveedor.Direccion = dto.Direccion;
            proveedor.Ciudad = dto.Ciudad;
            proveedor.Estado = dto.Estado;
            proveedor.Contacto = dto.Contacto;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound("El proveedor solicitado no existe.");
            }

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
