using FluentValidation;
using PetLoveAPI.DTOs.Proveedor;

namespace PetLoveAPI.Validators
{
    public class ProveedorValidator : AbstractValidator<AccionesProveedorDTO>
    {
        public ProveedorValidator()
        {
            RuleFor(p => p.Empresa)
                .NotEmpty().WithMessage("El nombre de la empresa es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre de la empresa no debe superar los 100 caracteres.")
                .Matches("^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\\s]+$").WithMessage("Solo se permiten letras, números y espacios.");

            RuleFor(p => p.TipoDocumento)
                .NotEmpty().WithMessage("El tipo de documento es obligatorio.")
                .Must(t => t == "CC" || t == "NIT" || t == "CE" || t == "TI")
                .WithMessage("Tipo de documento inválido (permitidos: CC, NIT, CE, TI).");

            RuleFor(p => p.Documento)
                .NotEmpty().WithMessage("El número de documento es obligatorio.")
                .MaximumLength(20).WithMessage("El documento no debe superar los 20 caracteres.")
                .Matches("^[0-9]+$").WithMessage("El documento solo puede contener números.");

            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El nombre del proveedor es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no debe superar los 100 caracteres.")
                .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ\\s]+$").WithMessage("El nombre solo puede contener letras y espacios.");

            RuleFor(p => p.Correo)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .EmailAddress().WithMessage("El correo no tiene un formato válido.");

            RuleFor(p => p.Celular)
                .NotEmpty().WithMessage("El número de celular es obligatorio.")
                .Matches("^[0-9]{7,15}$").WithMessage("El celular debe contener entre 7 y 15 dígitos numéricos.");

            RuleFor(p => p.Direccion)
                .NotEmpty().WithMessage("La dirección es obligatoria.")
                .MaximumLength(150).WithMessage("La dirección no debe superar los 150 caracteres.");

            RuleFor(p => p.Ciudad)
                .NotEmpty().WithMessage("La ciudad es obligatoria.")
                .MaximumLength(100).WithMessage("La ciudad no debe superar los 100 caracteres.")
                .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ\\s]+$").WithMessage("La ciudad solo puede contener letras y espacios.");

            RuleFor(p => p.Estado)
                .NotNull().WithMessage("El estado es obligatorio.");

            RuleFor(p => p.Contacto)
                .MaximumLength(100).WithMessage("El nombre del contacto no debe superar los 100 caracteres.")
                .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ\\s]*$").WithMessage("El contacto solo puede contener letras y espacios.")
                .When(p => !string.IsNullOrWhiteSpace(p.Contacto));
        }
    }
}
