using FluentValidation;
using PetLoveAPI.DTOs.Venta;

namespace PetLoveAPI.Validators
{
    public class CrearVentaValidator : AbstractValidator<CrearVentaDTO>
    {
        public CrearVentaValidator()
        {
            RuleFor(v => v.Usuario)
                .GreaterThan(0).WithMessage("El usuario es obligatorio y debe ser válido.");

            RuleFor(v => v.Fecha)
                .NotEmpty().WithMessage("La fecha de la venta es obligatoria.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("La fecha no puede ser mayor a la actual.");

            RuleFor(v => v.MetodoPago)
                .GreaterThan(0).WithMessage("Debe seleccionar un método de pago válido.");

            RuleFor(v => v.Descuento)
                .GreaterThanOrEqualTo(0).WithMessage("El descuento no puede ser negativo.")
                .LessThanOrEqualTo(100).WithMessage("El descuento no puede ser mayor al 100%.");

            RuleFor(v => v.Observaciones)
                .MaximumLength(250).WithMessage("Las observaciones no deben superar los 250 caracteres.")
                .When(v => !string.IsNullOrWhiteSpace(v.Observaciones));

            RuleFor(v => v.Estado)
                .GreaterThan(0).WithMessage("El estado de la venta es obligatorio.");

            RuleFor(v => v.Detalles)
                .NotEmpty().WithMessage("La venta debe contener al menos un detalle (producto).");

            RuleForEach(v => v.Detalles)
                .SetValidator(new CrearDetalleVentaValidator());
        }
    }

    public class CrearDetalleVentaValidator : AbstractValidator<CrearDetalleVentaDTO>
    {
        public CrearDetalleVentaValidator()
        {
            RuleFor(d => d.Producto)
                .GreaterThan(0).WithMessage("El producto es obligatorio y debe ser válido.");

            RuleFor(d => d.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");
        }
    }
}
