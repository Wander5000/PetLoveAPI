using FluentValidation;
using PetLove.Server.Dtos.Clientes;

namespace PetLove.Server.Validators
{
    public class ClientesValidator : AbstractValidator<CrearClienteDto>
    {
        public ClientesValidator()
        {
            RuleFor(x => x.TipoDocumento)
            .NotEmpty().WithMessage("El tipo de documento es obligatorio")
            .MaximumLength(10).WithMessage("Máximo 10 caracteres");

            RuleFor(x => x.NumeroDocumento)
                .NotEmpty().WithMessage("El número de documento es obligatorio")
                .Length(5, 20).WithMessage("Debe tener entre 5 y 20 caracteres")
                .Matches("^[0-9]+$").WithMessage("Solo se permiten números");

            RuleFor(x => x.Nombres)
                .NotEmpty().WithMessage("Los nombres son obligatorios")
                .MaximumLength(100).WithMessage("Máximo 100 caracteres")
                .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ\\s]+$").WithMessage("Solo se permiten letras");

            RuleFor(x => x.Apellidos)
                .NotEmpty().WithMessage("Los apellidos son obligatorios")
                .MaximumLength(100).WithMessage("Máximo 100 caracteres")
                .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ\\s]+$").WithMessage("Solo se permiten letras");

            RuleFor(x => x.Correo)
                .NotEmpty().WithMessage("El correo es obligatorio")
                .EmailAddress().WithMessage("Formato de correo no válido");

            RuleFor(x => x.Celular)
                .NotEmpty().WithMessage("El celular es obligatorio")
                .Matches("^[0-9]{10}$").WithMessage("Debe tener exactamente 10 dígitos");

            RuleFor(x => x.Municipio)
                .NotEmpty().WithMessage("El municipio es obligatorio");

            RuleFor(x => x.Direccion)
                .NotEmpty().WithMessage("La dirección es obligatoria")
                .MinimumLength(5).WithMessage("Debe tener al menos 5 caracteres");

        }

    }
}
