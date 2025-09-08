using FluentValidation;
using PetLoveAPI.DTOs.Compra;

namespace PetLoveAPI.Validators
{
    public class CrearCompraValidator : AbstractValidator<CrearCompraDTO>
    {
        public CrearCompraValidator()
        {
            RuleFor(c => c.Proveedor)
                .GreaterThan(0).WithMessage("El proveedor es obligatorio y debe ser válido.");

            RuleFor(c => c.FechaCompra)
                .NotEmpty().WithMessage("La fecha de la compra es obligatoria.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("La fecha no puede ser mayor a la actual.");

            RuleFor(c => c.Total)
                .GreaterThan(0).WithMessage("El total de la compra debe ser mayor a 0.");

            RuleFor(c => c.Estado)
                .NotNull().WithMessage("El estado de la compra es obligatorio.");

            RuleFor(c => c.Detalles)
                .NotEmpty().WithMessage("La compra debe contener al menos un detalle (producto).");

            RuleForEach(c => c.Detalles)
                .SetValidator(new CrearDetalleCompraValidator());
        }
    }

    public class CrearDetalleCompraValidator : AbstractValidator<CrearDetalleCompraDTO>
    {
        public CrearDetalleCompraValidator()
        {
            RuleFor(d => d.Producto)
                .GreaterThan(0).WithMessage("El producto es obligatorio y debe ser válido.");

            RuleFor(d => d.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");
        }
    }
}
