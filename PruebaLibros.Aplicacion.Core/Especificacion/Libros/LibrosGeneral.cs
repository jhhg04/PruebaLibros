using PruebaLibros.Aplicacion.Core.Especificacion;
using PruebaLibros.Dominio.Principal.Entidades;

namespace PruebaLibros.Aplicacion.Core.Especificacion.Libros;

public class LibrosGeneral : BaseEspecificacion<Libro>
{
    public LibrosGeneral(LibrosParametros parametros)
        : base(
            x =>
                //Busqueda principal por conincidencia de parametro nombre
                (string.IsNullOrEmpty(parametros.Busqueda) || x.Titulo.Contains(parametros.Busqueda) || x.Genero.Contains(parametros.Busqueda) || x.Autor.NombreCompleto.Contains(parametros.Busqueda)) &&
                (string.IsNullOrEmpty(parametros.Titulo) || x.Titulo == parametros.Titulo) &&
                (string.IsNullOrEmpty(parametros.Genero) || x.Genero == parametros.Genero) &&
                (string.IsNullOrEmpty(parametros.NombreAutor) || x.Autor.NombreCompleto == parametros.NombreAutor) &&
                (!(parametros.IdAutor.HasValue) || x.Autor.IdAutorPk == parametros.IdAutor) &&
                (!(parametros.Ano.HasValue) || x.Año == parametros.Ano)
        )
    {
        AplicarPaginacion(parametros.TamanoPagina * (parametros.IndicePagina - 1), parametros.TamanoPagina);
        AgregarInclusion(x => x.Autor);

        if (!string.IsNullOrEmpty(parametros.Ordenar))
        {
            switch (parametros.Ordenar)
            {
                case "tituloAsc":
                    AgregarOrdenAsc(c => c.Titulo!);
                    break;
                case "tituloDesc":
                    AgregarOrdenDesc(c => c.Titulo!);
                    break;
                default:
                    AgregarOrdenAsc(c => c.Titulo!);
                    break;
            }
        }
    }
    public LibrosGeneral(int id) : base(x => x.IdLibroPk == id)
    {
        AgregarInclusion(x => x.Autor);
    }

    public LibrosGeneral()
    {
        AgregarInclusion(x => x.Autor);
    }
}
