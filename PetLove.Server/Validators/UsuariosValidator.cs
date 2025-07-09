using FluentValidation;
using PetLove.Server.Dtos.Usuarios;

namespace PetLove.Server.Validators
{
    public class UsuariosValidator : AbstractValidator<CrearUsuarioDto>
    {
        public UsuariosValidator()
        {
            RuleFor(x => x.NombreUsuario)
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre de usuario no debe superar los 50 caracteres.");

            RuleFor(x => x.Contraseña)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("Debe tener al menos 6 caracteres.")
                .Matches(@"[A-Z]").WithMessage("Debe contener al menos una letra mayúscula.")
                .Matches(@"[a-z]").WithMessage("Debe contener al menos una letra minúscula.")
                .Matches(@"\d").WithMessage("Debe contener al menos un número.");

            RuleFor(x => x.Correo)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .EmailAddress().WithMessage("El correo no tiene un formato válido.");

            RuleFor(x => x.Rol)
                .GreaterThan(0).WithMessage("Debe seleccionar un rol válido.");
        }
    }

    public class ActualizarUsuarioValidator : AbstractValidator<ActualizarUsuarioDto>
    {
        public ActualizarUsuarioValidator()
        {
            RuleFor(x => x.NombreUsuario)
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre de usuario no debe superar los 50 caracteres.");

            RuleFor(x => x.Correo)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .EmailAddress().WithMessage("El correo no tiene un formato válido.");

            RuleFor(x => x.Rol)
                .GreaterThan(0).WithMessage("Debe seleccionar un rol válido.");
        }
    }
}
