using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLove.Server.Context;
using PetLove.Server.Dtos.Clientes;
using PetLove.Server.Models;

namespace PetLove.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly PetLoveContext _context;

        public ClientesController(PetLoveContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> ListarClientes()
        {
            return await _context.Clientes
                .Select(c => new ClienteDto
                {
                    IdCliente = c.IdCliente,
                    NumeroDocumento = c.NumeroDocumento,
                    Nombres = c.Nombres,
                    Apellidos = c.Apellidos,
                    Correo = c.Correo,
                    Celular = c.Celular
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<CrearClienteDto>> CrearCliente(CrearClienteDto CrearClienteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Error en la solicitud. Verifica los campos e intenta nuevamente.");
            }

            var cliente = new Cliente
            {
                Usuario = CrearClienteDto.Usuario,
                TipoDocumento = CrearClienteDto.TipoDocumento,
                NumeroDocumento = CrearClienteDto.NumeroDocumento,
                Nombres = CrearClienteDto.Nombres,
                Apellidos = CrearClienteDto.Apellidos,
                Correo = CrearClienteDto.Correo,
                Celular = CrearClienteDto.Celular,
                Municipio = CrearClienteDto.Municipio,
                Direccion = CrearClienteDto.Direccion,

            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(ListarClientes), new { id = cliente.IdCliente }, CrearClienteDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarCliente(int id, CrearClienteDto clienteDto)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound("El cliente solicitado no existe.");
            }

            cliente.Usuario = clienteDto.Usuario;
            cliente.TipoDocumento = clienteDto.TipoDocumento;
            cliente.NumeroDocumento = clienteDto.NumeroDocumento;
            cliente.Nombres = clienteDto.Nombres;
            cliente.Apellidos = clienteDto.Apellidos;
            cliente.Correo = clienteDto.Correo;
            cliente.Celular = clienteDto.Celular;
            cliente.Municipio = clienteDto.Municipio;
            cliente.Direccion = clienteDto.Direccion;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound("El cliente solicitado no existe.");
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
