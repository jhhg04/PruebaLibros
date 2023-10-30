using FluentValidation;
using PruebaLibros.Aplicacion.DTO;

namespace PruebaLibros.Aplicacion.Core.Validadores;

public class LibroValidador : AbstractValidator<In_LibroDTO>
{
    public LibroValidador()
    {
        RuleFor(l => l.Titulo)
            .NotNull().WithMessage("El campo titulo , es obligatorio")
            .MinimumLength(3).WithMessage("El campo titulo, debe contener al menos 3 caracteres");

        RuleFor(b => b.IdAutor)
                .NotNull().WithMessage("El campo IdAutor, es obligatorio")
                .GreaterThan(0).WithMessage("El campo IdAutor, debe ser mayor que cero");

        RuleFor(b => b.NumPaginas)
                .NotNull().WithMessage("El campo NumPaginas, es obligatorio")
                .GreaterThan(0).WithMessage("El campo NumPaginas, debe ser mayor que cero");

        RuleFor(b => b.Año)
                .NotNull().WithMessage("El campo Año, es obligatorio")
                .GreaterThan(0).WithMessage("El campo Año, debe ser mayor que cero");

        RuleFor(b => b.Genero)
                .NotNull().WithMessage("El campo Genero, es obligatorio");
    }
}
