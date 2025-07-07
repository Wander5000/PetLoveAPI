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
        public async Task<ActionResult<ClienteDto>> CrearCliente(ClienteDto clienteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Error en la solicitud. Verifica los campos e intenta nuevamente.");
            }

            var cliente = new Cliente
            {
                NumeroDocumento = clienteDto.NumeroDocumento,
                Nombres = clienteDto.Nombres,
                Apellidos = clienteDto.Apellidos,
                Correo = clienteDto.Correo,
                Celular = clienteDto.Celular
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(ListarClientes), new { id = cliente.IdCliente }, clienteDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarCliente(int id, ClienteDto clienteDto)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound("El cliente solicitado no existe.");
            }

            cliente.NumeroDocumento = clienteDto.NumeroDocumento;
            cliente.Nombres = clienteDto.Nombres;
            cliente.Apellidos = clienteDto.Apellidos;
            cliente.Correo = clienteDto.Correo;
            cliente.Celular = clienteDto.Celular;

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
