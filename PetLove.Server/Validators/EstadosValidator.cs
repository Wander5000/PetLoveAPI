using FluentValidation;
using PetLove.Server.Dtos.Estados;

namespace PetLove.Server.Validators
{
    public class EstadosValidator : AbstractValidator<AccionesEstadoDto>
    {
        public EstadosValidator() 
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MinimumLength(2).WithMessage("El nombre debe tener al menos 2 caracteres.")
                .MaximumLength(50).WithMessage("El nombre no puede tener más de 50 caracteres.")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$").WithMessage("El nombre solo puede contener letras y espacios.");
        }
    }
}
