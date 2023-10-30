using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PruebaLibros.Aplicacion.Core.Adaptadores;
using PruebaLibros.Aplicacion.Core.Error;
using PruebaLibros.Aplicacion.Core.Especificacion.Autores;
using PruebaLibros.Aplicacion.Core.Interfaces;
using PruebaLibros.Aplicacion.DTO;
using PruebaLibros.Dominio.Principal.Entidades;

namespace PruebaLibros.Presentacion.ApiREST.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutorController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    IValidator<In_AutorDTO> _validator;

    public AutorController(IUnitOfWork unitOfWork, IValidator<In_AutorDTO> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    [HttpGet("GetAll")]
    public async Task<IResult> GetAll()
    {
        var listObj = await _unitOfWork.Repositorio<Autor>().GetAllAsync();
        if (listObj is null || listObj?.Count == 0)
            return Results.NotFound(new CodigoErrorException(404, "Sin datos"));

        return Results.Ok(listObj);
    }

    [HttpGet("GetById")]
    public async Task<IResult> GetById([FromQuery] int id)
    {
        var res = await _unitOfWork.Repositorio<Autor>().GetByIdAsync(id);
        if (res is null)
            return Results.NotFound(new CodigoErrorException(404, "El autor no se ha encontrado", $"idAutor = {id}"));

        return Results.Ok(await _unitOfWork.Repositorio<Autor>().GetByIdAsync(id));
    }

    [HttpGet("GetAll_Paginacion")]
    public async Task<IResult> GetAll_Paginacion([FromQuery] AutoresParametros parametros)
    {
        var especificacion = new AutoresGeneral(parametros);
        var listObj = await _unitOfWork.Repositorio<Autor>().GetAll_ConEspecificacionAsync(especificacion);
        var conteoList = await _unitOfWork.Repositorio<Autor>().ConteoAsync(especificacion);

        var rounded = Math.Ceiling(Convert.ToDecimal(conteoList / parametros.TamanoPagina));
        var totalPaginas = Convert.ToInt32(rounded) == 0 ? 1 : Convert.ToInt32(rounded);

        if (listObj is null || listObj?.Count == 0)
            return Results.NotFound(new CodigoErrorException(404, "Sin datos"));

        return Results.Ok(
               new Paginacion<Autor>
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
    public async Task<IResult> Crear(In_AutorDTO objDTO)
    {
        var validationResult = _validator.Validate(objDTO);

        if (!validationResult.IsValid)
        {
            string errores = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Results.BadRequest(new CodigoErrorException(400, "Error de validacion en los datos de entrada", errores));
        }

        Autor newAutor = new()
        {
            NombreCompleto = objDTO.NombreCompleto,
            //FechaNacimiento = objDTO.FechaNacimiento.ToDateTime(TimeOnly.Parse("00:00")),
            FechaNacimiento = Convert.ToDateTime(objDTO.FechaNacimiento),
            Ciudad = objDTO.Ciudad,
            Correo = objDTO.Correo
        };

        await _unitOfWork.Repositorio<Autor>().AgregarAsync(newAutor);

        return Results.Ok(newAutor);
    }

    [HttpPatch("Actualizar")]
    public async Task<IResult> Actualizar(int id, In_AutorDTO objDTO)
    {
        var validationResult = _validator.Validate(objDTO);

        if (!validationResult.IsValid)
        {
            string errores = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Results.BadRequest(new CodigoErrorException(400, "Error de validacion en los datos de entrada", errores));
        }

        var objActualizar = await _unitOfWork.Repositorio<Autor>().GetByIdAsync(id);

        objActualizar.NombreCompleto = objDTO.NombreCompleto;
        objActualizar.FechaNacimiento = Convert.ToDateTime(objDTO.FechaNacimiento);
        objActualizar.Ciudad = objDTO.Ciudad;
        objActualizar.Correo = objDTO.Correo;

        await _unitOfWork.Repositorio<Autor>().ActualizarAsync(objActualizar);

        return Results.Ok(objActualizar);
    }

    [HttpDelete("Eliminar")]
    public async Task<IResult> Eliminar([FromQuery] int id)
    {

        var objEliminar = await _unitOfWork.Repositorio<Autor>().GetByIdAsync(id);

        if (objEliminar is null)
            return Results.NotFound(new CodigoErrorException(404, "El autor no se ha encontrado", $"idAutor = {id}"));

        await _unitOfWork.Repositorio<Autor>().EliminarAsync(objEliminar);

        return Results.Ok(id);
    }
}
