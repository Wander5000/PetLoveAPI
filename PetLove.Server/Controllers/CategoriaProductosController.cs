using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetLove.Server.Context;
using PetLove.Server.Models;
using PetLove.Server.Dtos.CategoriasProducto;
using Microsoft.EntityFrameworkCore;

namespace PetLove.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriaProductosController : ControllerBase
    {
        private readonly PetLoveContext _context;

        public CategoriaProductosController(PetLoveContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaProductoDto>>> ListarCategorias()
        {
            return await _context.CategoriaProductos
                .Select(c => new CategoriaProductoDto
                {
                    IdCategoria = c.IdCategoria,
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaProductoDto>> CrearCategoria(AccionesCategoriaProductoDto crearCategoriaDto)
        {
            var categoria = new CategoriaProducto
            {
                Nombre = crearCategoriaDto.Nombre,
                Descripcion = crearCategoriaDto.Descripcion
            };
            _context.CategoriaProductos.Add(categoria);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarCategorias), new { id = categoria.IdCategoria }, crearCategoriaDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCategoria(int id, AccionesCategoriaProductoDto actualizarCategoriaDto)
        {
            var categoria = await _context.CategoriaProductos.FindAsync(id);
            if (categoria == null)
            {
                return NotFound("la categoria Solicitada es Erronea o Inexistente");
            }
            categoria.Nombre = actualizarCategoriaDto.Nombre;
            categoria.Descripcion = actualizarCategoriaDto.Descripcion;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            var categoria = await _context.CategoriaProductos.FindAsync(id);
            if (categoria == null)
            {
                return NotFound("la categoria Solicitada es Erronea o Inexistente");
            }
            _context.CategoriaProductos.Remove(categoria);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
