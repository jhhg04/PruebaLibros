using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PruebaLibros.Aplicacion.Core.Interfaces;
using PruebaLibros.Aplicacion.Core.IOC;
using PruebaLibros.Infraestructura.Data;
using PruebaLibros.Infraestructura.Data.Repositorios;
using PruebaLibros.Presentacion.ApiREST.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
  .MinimumLevel.Information()
  .WriteTo.File("logs/log-system.txt", rollingInterval: RollingInterval.Hour)
  .CreateLogger();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepositorioGenerico<>), (typeof(RepositorioGenerico<>)));
builder.Services.AddDbContext<ContextoBD>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AgregarValidadores();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsRule", rule =>
    {
        rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
    });
});

builder.Logging.AddEventLog();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var dataContext = scope.ServiceProvider.GetRequiredService<ContextoBD>();
        await dataContext.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        Log.Error($"Errores en el proceso de migracion: {ex.Message} - {(ex.InnerException != null ? ex.InnerException.Message : "")}");
    }
}



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();
app.UseCors("CorsRule");
app.MapControllers();

app.Run();
