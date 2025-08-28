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
        public async Task<ActionResult<IEnumerable<Proveedor>>> ListarProveedores()
        {
            return await _context.Proveedores
                .Select(p => new Proveedor
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
        }

        [HttpPost]
        public async Task<ActionResult<ProveedorDTO>> CrearProveedor(ProveedorDTO proveedorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var proveedor = new Proveedor
            {
                IdProveedor = proveedorDto.IdProveedor,
                Empresa = proveedorDto.Empresa,
                TipoDocumento = proveedorDto.TipoDocumento,
                Documento = proveedorDto.Documento,
                Nombre = proveedorDto.Nombre,
                Correo = proveedorDto.Correo,
                Celular = proveedorDto.Celular,
                Direccion = proveedorDto.Direccion,
                Ciudad = proveedorDto.Ciudad,
                Estado = proveedorDto.Estado,
                Contacto = proveedorDto.Contacto
            };

            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ListarProveedores), new { id = proveedor.IdProveedor }, proveedorDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarProveedor(int id, AccionesProveedorDTO proveedorDto)
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

            proveedor.Empresa = proveedorDto.Empresa;
            proveedor.TipoDocumento = proveedorDto.TipoDocumento;
            proveedor.Documento = proveedorDto.Documento;
            proveedor.Nombre = proveedorDto.Nombre;
            proveedor.Correo = proveedorDto.Correo;
            proveedor.Celular = proveedorDto.Celular;
            proveedor.Direccion = proveedorDto.Direccion;
            proveedor.Ciudad = proveedorDto.Ciudad;
            proveedor.Estado = proveedorDto.Estado;
            proveedor.Contacto = proveedorDto.Contacto;

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
