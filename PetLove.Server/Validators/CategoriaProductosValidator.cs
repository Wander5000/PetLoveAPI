using FluentValidation;
using PetLove.Server.Dtos.CategoriasProducto;

namespace PetLove.Server.Validators
{
    public class CategoriaProductosValidator
    {
        public class CrearCategoriaProductoValidator : AbstractValidator<AccionesCategoriaProductoDto>
        {
            public CrearCategoriaProductoValidator()
            {
                RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(50).WithMessage("Máximo 50 caracteres")
                .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ\\s]+$").WithMessage("Solo se permiten letras");

                RuleFor(x => x.Descripcion)
                    .NotEmpty().WithMessage("La descripción es obligatoria")
                    .MinimumLength(5).WithMessage("Debe tener al menos 5 caracteres")
                    .MaximumLength(200).WithMessage("Máximo 200 caracteres permitidos");
            }
        }

    }
}
