using FluentValidation;
using PetLove.Server.Dtos.Proveedores;

namespace PetLove.Server.Validators
{
    public class ProveedoresValidator : AbstractValidator<AccionesProveedorDto>
    {
        public ProveedoresValidator()
        {
            RuleFor(x => x.Empresa)
                .NotEmpty().WithMessage("El nombre de la empresa es obligatorio.")
                .MaximumLength(100).WithMessage("La empresa no debe superar los 100 caracteres.");

            RuleFor(x => x.TipoDocumento)
                .NotEmpty().WithMessage("El tipo de documento es obligatorio.")
                .MaximumLength(10).WithMessage("Máximo 10 caracteres para el tipo de documento.");

            RuleFor(x => x.Documento)
                .NotEmpty().WithMessage("El número de documento es obligatorio.")
                .Length(5, 20).WithMessage("Debe tener entre 5 y 20 caracteres.")
                .Matches("^[0-9]+$").WithMessage("Solo se permiten números en el documento.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("Máximo 100 caracteres.")
                .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ\\s]+$").WithMessage("Solo se permiten letras y espacios.");

            RuleFor(x => x.Correo)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .EmailAddress().WithMessage("Formato de correo no válido.");

            RuleFor(x => x.Celular)
                .NotEmpty().WithMessage("El celular es obligatorio.")
                .Matches("^[0-9]{10}$").WithMessage("El celular debe tener exactamente 10 dígitos.");

            RuleFor(x => x.Direccion)
                .NotEmpty().WithMessage("La dirección es obligatoria.")
                .MinimumLength(5).WithMessage("Debe tener al menos 5 caracteres.");

            RuleFor(x => x.Ciudad)
                .NotEmpty().WithMessage("La ciudad es obligatoria.");

            RuleFor(x => x.Contacto)
                .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ\\s]*$").WithMessage("El nombre del contacto solo puede contener letras y espacios.")
                .When(x => !string.IsNullOrWhiteSpace(x.Contacto));
        }
    }
}
