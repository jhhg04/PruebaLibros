using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PruebaLibros.Aplicacion.Core.Servicios;
using PruebaLibros.Aplicacion.DTO;
using PruebaLibros.Dominio.Principal.Entidades;
using PruebaLibros.Presentacion.WebApp.Models;
using System.Diagnostics;
using System.Text.Json;

namespace PruebaLibros.Presentacion.WebApp.Controllers
{
    public class AutorController : Controller
    {
        private readonly ILogger<AutorController> _logger;
        private readonly ApiHttpService _apiHttpService;
        private readonly IMapper _mapper;

        public AutorController(ILogger<AutorController> logger, ApiHttpService apiHttpService, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _apiHttpService = apiHttpService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var httpResponse = await _apiHttpService.GetAsync<List<Autor>>("api/Autor/GetAll");

            if (httpResponse == null)
            {
                TempData["Error"] = "Error general consultando autores";
                return View();
            }
            if (httpResponse!.Error)
            {
                var message = await httpResponse.GetErrorMessageAsync();
                try
                {
                    var objError = JsonSerializer.Deserialize<ErrorDTO>(message!);
                    TempData["Error"] = objError!.mensaje;
                    if (objError.detalles is not null)
                    {
                        List<string> listError = objError.detalles.Split('|').ToList();
                        TempData["Opciones"] = listError;
                    }
                }
                catch
                {
                    TempData["Error"] = message;
                }
                return View();
            }

            var listadoLibros = httpResponse.Response!;
            var objRespuesta = _mapper.Map<List<Autor>, IEnumerable<AutorDTO>>(listadoLibros);

            return View(objRespuesta);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearAsync(In_AutorDTO obj)
        {
            if (ModelState.IsValid)
            {
                var httpResponse = await _apiHttpService.PostAsync<In_AutorDTO>("api/Autor/Crear", obj);

                if (httpResponse == null)
                {
                    TempData["Error"] = "Error al registrar el Autor";
                    return View();
                }
                if (httpResponse!.Error)
                {
                    var message = await httpResponse.GetErrorMessageAsync();
                    try
                    {
                        var objError = JsonSerializer.Deserialize<ErrorDTO>(message!);
                        TempData["Error"] = objError!.mensaje;
                        if (objError.detalles is not null)
                        {
                            List<string> listError = objError.detalles.Split('|').ToList();
                            TempData["Opciones"] = listError;
                        }
                    }
                    catch
                    {
                        TempData["Error"] = message;
                    }
                    return View();
                }

                //var res = httpResponse.Response!;
                TempData["Ok"] = "Registro creado satisfactoriamente";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditarAsync(int id)
        {
            var httpResponse = await _apiHttpService.GetAsync<Autor>($"api/Autor/GetById?id={id}");

            if (httpResponse == null)
            {
                TempData["Error"] = "Error al consultar el Autor";
                return View();
            }
            if (httpResponse!.Error)
            {
                var message = await httpResponse.GetErrorMessageAsync();
                try
                {
                    var objError = JsonSerializer.Deserialize<ErrorDTO>(message!);
                    TempData["Error"] = objError!.mensaje;
                    if (objError.detalles is not null)
                    {
                        List<string> listError = objError.detalles.Split('|').ToList();
                        TempData["Opciones"] = listError;
                    }
                }
                catch
                {
                    TempData["Error"] = message;
                }
                return View();
            }

            var res = httpResponse.Response!;
            var objEditar = _mapper.Map<Autor, AutorDTO>(res);
            return View(objEditar);
        }

        [HttpPost]
        public async Task<IActionResult> EditarAsync(AutorDTO obj)
        {
            if (ModelState.IsValid)
            {
                var objEditar = _mapper.Map<AutorDTO, In_AutorDTO>(obj);
                var httpResponse = await _apiHttpService.PatchAsync<In_AutorDTO>($"api/Autor/Actualizar?id={obj.IdAutor}", objEditar);

                if (httpResponse == null)
                {
                    TempData["Error"] = "Error al registrar el Autor";
                    return View();
                }
                if (httpResponse!.Error)
                {
                    var message = await httpResponse.GetErrorMessageAsync();
                    try
                    {
                        var objError = JsonSerializer.Deserialize<ErrorDTO>(message!);
                        TempData["Error"] = objError!.mensaje;
                        if (objError.detalles is not null)
                        {
                            List<string> listError = objError.detalles.Split('|').ToList();
                            TempData["Opciones"] = listError;
                        }
                    }
                    catch
                    {
                        TempData["Error"] = message;
                    }
                    return View();
                }

                //var res = httpResponse.Response!;
                TempData["Ok"] = "Registro actualizado satisfactoriamente";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DetalleAsync(int id)
        {
            var httpResponse = await _apiHttpService.GetAsync<Autor>($"api/Autor/GetById?id={id}");

            if (httpResponse == null)
            {
                TempData["Error"] = "Error al consultar el Autor";
                return View();
            }
            if (httpResponse!.Error)
            {
                var message = await httpResponse.GetErrorMessageAsync();
                try
                {
                    var objError = JsonSerializer.Deserialize<ErrorDTO>(message!);
                    TempData["Error"] = objError!.mensaje;
                    if (objError.detalles is not null)
                    {
                        List<string> listError = objError.detalles.Split('|').ToList();
                        TempData["Opciones"] = listError;
                    }
                }
                catch
                {
                    TempData["Error"] = message;
                }
                return View();
            }

            var res = httpResponse.Response!;
            var objDetalle = _mapper.Map<Autor, AutorDTO>(res);
            return View(objDetalle);
        }

        [HttpGet]
        public async Task<IActionResult> EliminarAsync(int id)
        {
            var httpResponse = await _apiHttpService.GetAsync<Autor>($"api/Autor/GetById?id={id}");

            if (httpResponse == null)
            {
                TempData["Error"] = "Error al consultar el Autor";
                return View();
            }
            if (httpResponse!.Error)
            {
                var message = await httpResponse.GetErrorMessageAsync();
                try
                {
                    var objError = JsonSerializer.Deserialize<ErrorDTO>(message!);
                    TempData["Error"] = objError!.mensaje;
                    if (objError.detalles is not null)
                    {
                        List<string> listError = objError.detalles.Split('|').ToList();
                        TempData["Opciones"] = listError;
                    }
                }
                catch
                {
                    TempData["Error"] = message;
                }
                return View();
            }

            var res = httpResponse.Response!;
            var objEliminar = _mapper.Map<Autor, AutorDTO>(res);
            return View(objEliminar);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarAsync(AutorDTO obj)
        {
            var httpResponseLibros = await _apiHttpService.GetAsync<List<Libro>>($"api/Libro/GetAllFiltros?IdAutor={obj.IdAutor}");

            if (httpResponseLibros == null)
            {
                TempData["Error"] = "Error general consultando libros del autor";
                return View();
            }
            if (httpResponseLibros.Response is not null)
            {
                var listadoLibros = httpResponseLibros.Response!;

                if (listadoLibros is not null && listadoLibros.Count() > 0)
                {
                    TempData["Error"] = "No es posible eliminar el autor, existen los siguientes libros asociados";
                    TempData["Opciones"] = listadoLibros.Select(l => l.Titulo).Distinct().ToList();
                    return View();
                }
            }

            

            var httpResponse = await _apiHttpService.DeleteAsync($"api/Autor/Eliminar?id={obj.IdAutor}");

            if (httpResponse == null)
            {
                TempData["Error"] = "Error al eliminar el Autor";
                return View();
            }
            if (httpResponse!.Error)
            {
                var message = await httpResponse.GetErrorMessageAsync();
                try
                {
                    var objError = JsonSerializer.Deserialize<ErrorDTO>(message!);
                    TempData["Error"] = objError!.mensaje;
                    if (objError.detalles is not null)
                    {
                        List<string> listError = objError.detalles.Split('|').ToList();
                        TempData["Opciones"] = listError;
                    }
                }
                catch
                {
                    TempData["Error"] = message;
                }
                return View();
            }

            //var res = httpResponse.Response!;
            TempData["Ok"] = "Registro eliminado satisfactoriamente";
            return RedirectToAction(nameof(Index));

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}