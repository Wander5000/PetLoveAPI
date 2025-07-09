using FluentValidation;
using PetLove.Server.Dtos.Compras;

namespace PetLove.Server.Validators
{
    public class CompraValidator : AbstractValidator<CrearCompraDto>
    {
        public CompraValidator()
        {
            RuleFor(x => x.Proveedor)
                .GreaterThan(0).WithMessage("Debe seleccionar un proveedor válido.");

            RuleFor(x => x.FechaRegistro)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("La fecha de registro no puede ser futura.");

            RuleFor(x => x.FechaCompra)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("La fecha de compra no puede ser futura.");

            RuleFor(x => x.Total)
                .GreaterThan(0).WithMessage("El total debe ser mayor que cero.");

            RuleFor(x => x.Detalles)
                .NotEmpty().WithMessage("Debe registrar al menos un producto en la compra.");

            RuleForEach(x => x.Detalles).SetValidator(new DetallesCompraValidator());
        }
    }

    public class DetallesCompraValidator : AbstractValidator<CrearDetallesCompraDto>
    {
        public DetallesCompraValidator()
        {
            RuleFor(x => x.Producto)
                .GreaterThan(0).WithMessage("Debe seleccionar un producto válido.");

            RuleFor(x => x.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a cero.");
        }
    }
}
