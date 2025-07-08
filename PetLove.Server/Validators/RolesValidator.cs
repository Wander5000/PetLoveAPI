using FluentValidation;
using PetLove.Server.Dtos.Roles;

namespace PetLove.Server.Validators
{
    public class RolesValidator : AbstractValidator<CrearRolDto>
    {
        public RolesValidator()
        {
            RuleFor(x => x.NombreRol)
                .NotEmpty().WithMessage("El nombre del rol es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre del rol no debe superar los 50 caracteres.")
                .Matches("^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\\s]+$").WithMessage("Solo se permiten letras, números y espacios.");

            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MinimumLength(10).WithMessage("La descripción debe tener al menos 10 caracteres.")
                .MaximumLength(200).WithMessage("La descripción no debe superar los 200 caracteres.");
        }
    }
}
