using FluentValidation;
using PetLove.Server.Dtos.Productos;

namespace PetLove.Server.Validators
{
    public class ProductosValidator : AbstractValidator<AccionesProductoDto>
    {
        public ProductosValidator()
        {
            RuleFor(x => x.NombreProducto)
                .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre del producto no debe superar los 100 caracteres.")
                .Matches("^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\\s]+$").WithMessage("Solo se permiten letras, números y espacios.");

            RuleFor(x => x.Categoria)
                .GreaterThan(0).WithMessage("Debe seleccionar una categoría válida.");

            RuleFor(x => x.Medida)
                .GreaterThan(0).WithMessage("Debe seleccionar una medida válida.");

            RuleFor(x => x.Marca)
                .GreaterThan(0).WithMessage("Debe seleccionar una marca válida.");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo.");

            RuleFor(x => x.Cantidad)
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad no puede ser negativa.");

            RuleFor(x => x.Precio)
                .GreaterThan(0).WithMessage("El precio debe ser mayor que cero.");
        }
    }
}
