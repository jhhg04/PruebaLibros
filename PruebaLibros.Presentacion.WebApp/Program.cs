using PruebaLibros.Aplicacion.Core.Mapper;
using PruebaLibros.Aplicacion.Core.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(AutoMapperPerfil));

builder.Services.AddHttpClient<ApiHttpService>(client =>
{
    //client.BaseAddress = new Uri($"{builder.Configuration["ApiConfig:BaseLogin"]}{builder.Configuration["ApiConfig:Auth"]}");
    client.BaseAddress = new Uri(builder.Configuration["AppConfig:ApiUrl"]!);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Autor/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Autor}/{action=Index}/{id?}");

app.Run();
