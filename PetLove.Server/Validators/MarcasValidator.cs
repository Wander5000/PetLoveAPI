using FluentValidation;
using PetLove.Server.Dtos.Marcas;

namespace PetLove.Server.Validators
{
    public class MarcasValidator : AbstractValidator<AccionesMarcaDto>
    {
        public MarcasValidator()
        {
            RuleFor(x => x.NombreMarca)
                .NotEmpty().WithMessage("El nombre de la marca es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre de la marca no debe superar los 50 caracteres.")
                .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ\\s]+$").WithMessage("Solo se permiten letras y espacios.");
        }
    }
}
