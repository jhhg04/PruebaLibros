using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PruebaLibros.Aplicacion.Core.Adaptadores;
using PruebaLibros.Aplicacion.Core.Error;
using PruebaLibros.Aplicacion.Core.Especificacion.Libros;
using PruebaLibros.Aplicacion.Core.Interfaces;
using PruebaLibros.Aplicacion.DTO;
using PruebaLibros.Dominio.Principal.Entidades;

namespace PruebaLibros.Presentacion.ApiREST.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibroController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    IValidator<In_LibroDTO> _validator;

    public LibroController(IUnitOfWork unitOfWork, IValidator<In_LibroDTO> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    [HttpGet("GetAll")]
    public async Task<IResult> GetAll()
    {
        var especificacion = new LibrosGeneral();
        var listObj = await _unitOfWork.Repositorio<Libro>().GetAll_ConEspecificacionAsync(especificacion);
        if (listObj is null || listObj?.Count == 0)
            return Results.NotFound(new CodigoErrorException(404, "Sin datos"));

        return Results.Ok(listObj);
    }

    [HttpGet("GetAllFiltros")]
    public async Task<IResult> GetAllFiltros([FromQuery] LibrosParametros parametros)
    {
        var especificacion = new LibrosGeneral(parametros);
        var listObj = await _unitOfWork.Repositorio<Libro>().GetAll_ConEspecificacionAsync(especificacion);
        if (listObj is null || listObj?.Count == 0)
            return Results.NotFound(new CodigoErrorException(404, "Sin datos"));

        return Results.Ok(listObj);
    }

    [HttpGet("GetById")]
    public async Task<IResult> GetById([FromQuery] int id)
    {
        var especificacion = new LibrosGeneral(id);
        var res = await _unitOfWork.Repositorio<Libro>().GetById_ConEspecificacionAsync(especificacion);

        if (res is null)
            return Results.NotFound(new CodigoErrorException(404, "El libro no se ha encontrado", $"idLibro = {id}"));

        return Results.Ok(res);
    }

    [HttpGet("GetAll_Paginacion")]
    public async Task<IResult> GetAll_Paginacion([FromQuery] LibrosParametros parametros)
    {
        var especificacion = new LibrosGeneral(parametros);
        var listObj = await _unitOfWork.Repositorio<Libro>().GetAll_ConEspecificacionAsync(especificacion);
        var conteoList = await _unitOfWork.Repositorio<Libro>().ConteoAsync(especificacion);

        var rounded = Math.Ceiling(Convert.ToDecimal(conteoList / parametros.TamanoPagina));
        var totalPaginas = Convert.ToInt32(rounded) == 0 ? 1 : Convert.ToInt32(rounded);

        if (listObj is null || listObj?.Count == 0)
            return Results.NotFound(new CodigoErrorException(404, "Sin datos"));

        return Results.Ok(
               new Paginacion<Libro>
               {
                   Conteo = conteoList,
                   Datos = listObj,
                   TotalPaginas = totalPaginas,
                   IndicePagina = parametros.IndicePagina,
                   TamañoPagina = parametros.TamanoPagina
               }
           );
    }

    [HttpPost("Crear")]
    public async Task<IResult> Crear(In_LibroDTO objDTO)
    {
        var validationResult = _validator.Validate(objDTO);

        if (!validationResult.IsValid)
        {
            string errores = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Results.BadRequest(new CodigoErrorException(400, "Error de validacion en los datos de entrada", errores));
        }

        Libro newLibro = new()
        {
            Titulo = objDTO.Titulo,
            Año = objDTO.Año,
            Genero = objDTO.Genero,
            NumPaginas = objDTO.NumPaginas,
            IdAutorFk = objDTO.IdAutor
        };

        await _unitOfWork.Repositorio<Libro>().AgregarAsync(newLibro);

        return Results.Ok(newLibro);
    }

    [HttpPatch("Actualizar")]
    public async Task<IResult> Actualizar(int id, In_LibroDTO objDTO)
    {
        var validationResult = _validator.Validate(objDTO);

        if (!validationResult.IsValid)
        {
            string errores = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Results.BadRequest(new CodigoErrorException(400, "Error de validacion en los datos de entrada", errores));
        }

        var objActualizar = await _unitOfWork.Repositorio<Libro>().GetByIdAsync(id);

        objActualizar.Titulo = objDTO.Titulo;
        objActualizar.Año = objDTO.Año;
        objActualizar.Genero = objDTO.Genero;
        objActualizar.NumPaginas = objDTO.NumPaginas;
        objActualizar.IdAutorFk = objDTO.IdAutor;

        await _unitOfWork.Repositorio<Libro>().ActualizarAsync(objActualizar);

        return Results.Ok(objActualizar);
    }

    [HttpDelete("Eliminar")]
    public async Task<IResult> Eliminar([FromQuery] int id)
    {

        var objEliminar = await _unitOfWork.Repositorio<Libro>().GetByIdAsync(id);

        if (objEliminar is null)
            return Results.NotFound(new CodigoErrorException(404, "El libro no se ha encontrado", $"idLibro = {id}"));

        await _unitOfWork.Repositorio<Libro>().EliminarAsync(objEliminar);

        return Results.Ok(id);
    }
}
