using FluentValidation;
using PetLoveAPI.DTOs.Producto;

namespace PetLoveAPI.Validators
{
    public class ProductosValidator : AbstractValidator<AccionesProductoDTO>
    {
        public ProductosValidator()
        {
            RuleFor(p => p.NombreProducto)
                .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre del producto no debe superar los 100 caracteres.")
                .Matches("^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\\s]+$").WithMessage("Solo se permiten letras, números y espacios.");

            RuleFor(p => p.Categoria)
                .GreaterThan(0).WithMessage("Categoria Invalida.");

            RuleFor(p => p.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo.");

            RuleFor(p => p.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser un valor positivo.");

            RuleFor(p => p.Medida)
                .GreaterThan(0).WithMessage("Unidad de Medida Invalida.");

            RuleFor(p => p.Marca)
                .GreaterThan(0).WithMessage("Marca Invalida");

            RuleFor(p => p.Precio)
                .GreaterThan(0).WithMessage("El precio no puede ser negativo");

            RuleFor(p => p.Estado)
                .NotNull().WithMessage("El estado es obligatorio.");
        }
    }
}
