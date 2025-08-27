using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLoveAPI.Context;
using PetLoveAPI.Models;
using PetLoveAPI.DTOs.CategoriaProducto;

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
    }
}
