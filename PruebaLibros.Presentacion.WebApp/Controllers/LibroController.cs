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
    public class LibroController : Controller
    {
        private readonly ILogger<LibroController> _logger;
        private readonly ApiHttpService _apiHttpService;
        private readonly IMapper _mapper;

        public LibroController(ILogger<LibroController> logger, ApiHttpService apiHttpService, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _apiHttpService = apiHttpService;
        }
        public async Task SetViewBag()
        {
            TempData["Opciones"] = null;
            TempData["Error"] = null;
            var httpResponse = await _apiHttpService.GetAsync<List<Autor>>("api/Autor/GetAll");

            if (httpResponse == null)
            {
                TempData["Error"] = "Error General";
                return;
            }

            ViewBag.Autores = httpResponse.Response!;
        }


        public async Task<IActionResult> IndexAsync()
        {
            var httpResponse = await _apiHttpService.GetAsync<List<Libro>>("api/Libro/GetAll");

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
            var objRespuesta = _mapper.Map<List<Libro>, IEnumerable<LibroDTO>>(listadoLibros);

            return View(objRespuesta);
        }

        [HttpGet]
        public async Task<IActionResult> CrearAsync()
        {
            await SetViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearAsync(In_LibroDTO obj)
        {
            await SetViewBag();
            if (ModelState.IsValid)
            {
                var httpResponse = await _apiHttpService.PostAsync<In_LibroDTO>("api/Libro/Crear", obj);

                if (httpResponse == null)
                {
                    TempData["Error"] = "Error al registrar el Libro";
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
            var httpResponse = await _apiHttpService.GetAsync<Libro>($"api/Libro/GetById?id={id}");

            if (httpResponse == null)
            {
                TempData["Error"] = "Error al consultar el Libro";
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
            await SetViewBag();
            var res = httpResponse.Response!;
            var objEditar = _mapper.Map<Libro, LibroDTO>(res);
            return View(objEditar);
        }

        [HttpPost]
        public async Task<IActionResult> EditarAsync(LibroDTO obj)
        {
            await SetViewBag();
            if (ModelState.IsValid)
            {
                var objEditar = _mapper.Map<LibroDTO, In_LibroDTO>(obj);
                var httpResponse = await _apiHttpService.PatchAsync<In_LibroDTO>($"api/Libro/Actualizar?id={obj.IdAutor}", objEditar);

                if (httpResponse == null)
                {
                    TempData["Error"] = "Error al registrar el Libro";
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
            var httpResponse = await _apiHttpService.GetAsync<Libro>($"api/Libro/GetById?id={id}");

            if (httpResponse == null)
            {
                TempData["Error"] = "Error al consultar el Libro";
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
            var objDetalle = _mapper.Map<Libro, LibroDTO>(res);
            return View(objDetalle);
        }

        [HttpGet]
        public async Task<IActionResult> EliminarAsync(int id)
        {
            var httpResponse = await _apiHttpService.GetAsync<Libro>($"api/Libro/GetById?id={id}");

            if (httpResponse == null)
            {
                TempData["Error"] = "Error al consultar el Libro";
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
            var objEliminar = _mapper.Map<Libro, LibroDTO>(res);
            return View(objEliminar);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarAsync(LibroDTO obj)
        {

            var httpResponse = await _apiHttpService.DeleteAsync($"api/Libro/Eliminar?id={obj.IdLibro}");

            if (httpResponse == null)
            {
                TempData["Error"] = "Error al eliminar el Libro";
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