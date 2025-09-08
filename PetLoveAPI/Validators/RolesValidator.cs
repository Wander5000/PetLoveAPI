using FluentValidation;
using PetLoveAPI.DTOs.Rol;

namespace PetLoveAPI.Validators
{
    public class RolValidator : AbstractValidator<AccionesRolDTO>
    {
        public RolValidator()
        {
            RuleFor(r => r.NombreRol)
                .NotEmpty().WithMessage("El nombre del rol es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre del rol no debe superar los 50 caracteres.")
                .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ\\s]+$").WithMessage("El nombre del rol solo puede contener letras y espacios.");

            RuleFor(r => r.Descripcion)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MaximumLength(200).WithMessage("La descripción no debe superar los 200 caracteres.");
        }
    }
}

