using FluentValidation;
using PruebaLibros.Aplicacion.DTO;

namespace PruebaLibros.Aplicacion.Core.Validadores;

public class AutorValidador : AbstractValidator<In_AutorDTO>
{
    public AutorValidador()
    {
        RuleFor(l => l.NombreCompleto)
            .NotNull().WithMessage("El campo NombreCompleto , es obligatorio")
            .MinimumLength(3).WithMessage("El campo NombreCompleto, debe contener al menos 3 caracteres");

        RuleFor(b => b.FechaNacimiento)
                .NotNull().WithMessage("El campo FechaNacimiento, es obligatorio");

        RuleFor(b => b.Ciudad)
                .NotNull().WithMessage("El campo Ciudad, es obligatorio")
                .MinimumLength(3).WithMessage("El campo Ciudad, debe contener al menos 3 caracteres");

        RuleFor(b => b.Correo)
                .NotNull().WithMessage("El campo Correo, es obligatorio")
                .MinimumLength(6).WithMessage("El campo Correo, debe contener al menos 6 caracteres")
                .EmailAddress().WithMessage("El correo electrónico no es válido.");
    }
}
