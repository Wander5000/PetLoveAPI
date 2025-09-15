using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLoveAPI.Context;
using PetLoveAPI.DTOs.CategoriaProducto;
using PetLoveAPI.DTOs.CategoriaProducto;
using PetLoveAPI.Models;

namespace PetLoveAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasProductosController : ControllerBase
    {
        private readonly PetLoveApiContext _context;

        public CategoriasProductosController(PetLoveApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaProductoDTO>>> ListarCategorias()
        {
            return await _context.CategoriaProductos
            .Select(c => new CategoriaProductoDTO
            {
                IdCategoria = c.IdCategoria,
                NombreCategoria = c.NombreCategoria,
                Descripcion = c.Descripcion,
            })
            .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AccionesCategoriaProductoDTO>> CrearCategoria(AccionesCategoriaProductoDTO dto)
        {
            var categoria = new CategoriaProducto
            {
                NombreCategoria = dto.NombreCategoria,
                Descripcion = dto.Descripcion,
            };
            _context.CategoriaProductos.Add(categoria);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarCategorias), new { id = categoria.IdCategoria }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarTipoDoc(int id, AccionesCategoriaProductoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categoria = await _context.CategoriaProductos.FindAsync(id);
            if (categoria == null)
            {
                return NotFound("El Tipo de Documento Solicitado es Erroneo o Inexistente");
            }
            categoria.NombreCategoria = dto.NombreCategoria;
            categoria.Descripcion = dto.Descripcion;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarTipoDoc(int id)
        {
            var categoria = await _context.CategoriaProductos.FindAsync(id);
            if (categoria == null)
            {
                return NotFound("El Tipo de Documento Solicitado es Erroneo o Inexistente");
            }
            _context.CategoriaProductos.Remove(categoria);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
