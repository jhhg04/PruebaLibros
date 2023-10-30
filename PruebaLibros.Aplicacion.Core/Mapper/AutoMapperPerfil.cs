using AutoMapper;
using PruebaLibros.Aplicacion.DTO;
using PruebaLibros.Dominio.Principal.Entidades;

namespace PruebaLibros.Aplicacion.Core.Mapper;

public class AutoMapperPerfil : Profile
{
    public AutoMapperPerfil()
    {
        CreateMap<Autor, In_AutorDTO>().ReverseMap();
        CreateMap<AutorDTO, In_AutorDTO>().ReverseMap();

        CreateMap<Autor, AutorDTO>()
                .ForMember(destino => destino.IdAutor, opt => opt.MapFrom(origen => origen.IdAutorPk))
                .ForMember(destino => destino.FechaNacimiento, opt => opt.MapFrom(origen => origen.FechaNacimiento.ToString("yyyy-MM-dd")))
                .ReverseMap();


        CreateMap<Libro, In_LibroDTO>()
                .ForMember(destino => destino.IdAutor, opt => opt.MapFrom(origen => origen.IdAutorFk))
            .ReverseMap();

        CreateMap<LibroDTO, In_LibroDTO>().ReverseMap();

        CreateMap<Libro, LibroDTO>()
                .ForMember(destino => destino.IdLibro, opt => opt.MapFrom(origen => origen.IdLibroPk))
                .ForMember(destino => destino.IdAutor, opt => opt.MapFrom(origen => origen.IdAutorFk))
                .ReverseMap();
    }
}
