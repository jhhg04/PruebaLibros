using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PruebaLibros.Aplicacion.Core.Validadores;
using PruebaLibros.Aplicacion.DTO;

namespace PruebaLibros.Aplicacion.Core.IOC;

public static class InyectarValidadores
{
    public static IServiceCollection AgregarValidadores(this IServiceCollection services)
    {
        services.AddScoped<IValidator<In_AutorDTO>, AutorValidador>();
        services.AddScoped<IValidator<In_LibroDTO>, LibroValidador>();

        return services;
    }
}