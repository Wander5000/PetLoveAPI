using FluentValidation;
using PetLove.Server.Dtos.Ventas;

namespace PetLove.Server.Validators
{
    public class CrearVentaDtoValidator : AbstractValidator<CrearVentaDto>
    {
        public CrearVentaDtoValidator()
        {
            RuleFor(x => x.Cliente)
                .GreaterThan(0).WithMessage("Debe seleccionar un cliente válido.");

            RuleFor(x => x.Fecha)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("La fecha de la venta no puede ser en el futuro.");

            RuleFor(x => x.MetodoPago)
                .NotEmpty().WithMessage("El método de pago es obligatorio.")
                .MinimumLength(3).WithMessage("El método de pago debe tener al menos 3 caracteres.")
                .MaximumLength(50).WithMessage("El método de pago no puede superar los 50 caracteres.");

            RuleFor(x => x.Estado)
                .GreaterThan(0).WithMessage("Debe seleccionar un estado válido.");

            RuleFor(x => x.Detalles)
                .NotEmpty().WithMessage("Debe agregar al menos un producto en la venta.");

            RuleForEach(x => x.Detalles).SetValidator(new CrearDetallesVentaDtoValidator());
        }
    }

    public class CrearDetallesVentaDtoValidator : AbstractValidator<CrearDetallesVentaDto>
    {
        public CrearDetallesVentaDtoValidator()
        {
            RuleFor(x => x.Producto)
                .GreaterThan(0).WithMessage("Debe seleccionar un producto válido.");

            RuleFor(x => x.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a cero.");
        }
    }
}
