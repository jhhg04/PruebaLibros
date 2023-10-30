using PruebaLibros.Aplicacion.Core.Especificacion;
using PruebaLibros.Dominio.Principal.Entidades;

namespace PruebaLibros.Aplicacion.Core.Especificacion.Autores;

public class AutoresGeneral : BaseEspecificacion<Autor>
{
    public AutoresGeneral(AutoresParametros parametros)
        : base(
            x =>
                //Busqueda principal por conincidencia de parametro nombre
                (
                    string.IsNullOrEmpty(parametros.Busqueda) 
                    || x.NombreCompleto.Contains(parametros.Busqueda) 
                    || x.Ciudad.Contains(parametros.Busqueda) 
                    || x.Correo.Contains(parametros.Busqueda)
                ) &&
                (string.IsNullOrEmpty(parametros.NombreCompleto) || x.NombreCompleto == parametros.NombreCompleto) &&
                (string.IsNullOrEmpty(parametros.Ciudad) || x.Ciudad == parametros.Ciudad) &&
                (string.IsNullOrEmpty(parametros.Correo) || x.Correo == parametros.Correo)
        )
    {
        AplicarPaginacion(parametros.TamanoPagina * (parametros.IndicePagina - 1), parametros.TamanoPagina);

        if (!string.IsNullOrEmpty(parametros.Ordenar))
        {
            switch (parametros.Ordenar)
            {
                case "nombreAsc":
                    AgregarOrdenAsc(c => c.NombreCompleto!);
                    break;
                case "nombreDesc":
                    AgregarOrdenDesc(c => c.NombreCompleto!);
                    break;
                default:
                    AgregarOrdenAsc(c => c.NombreCompleto!);
                    break;
            }
        }
    }
    public AutoresGeneral(int id) : base(x => x.IdAutorPk == id)
    { }
}
