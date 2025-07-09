using FluentValidation;
using PetLove.Server.Dtos.Medidas;

namespace PetLove.Server.Validators
{
    public class MedidasValidator : AbstractValidator<AccionesMedidaDto>
    {
        public MedidasValidator()
        {
            RuleFor(x => x.NombreMedida)
                .NotEmpty().WithMessage("El nombre de la medida es obligatorio.")
                .MaximumLength(30).WithMessage("El nombre de la medida no debe superar los 30 caracteres.")
                .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ\\s]+$").WithMessage("Solo se permiten letras y espacios.");
        }
    }
}
