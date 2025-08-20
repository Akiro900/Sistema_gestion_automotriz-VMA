using Taller.Models;
using Taller.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));

builder.Services.AddSingleton<ClientesService>();
builder.Services.AddSingleton<VehiculosService>();
builder.Services.AddSingleton<MantenimientosService>();
builder.Services.AddSingleton<CitasService>();
builder.Services.AddSingleton<ProveedoresService>();
builder.Services.AddSingleton<EmpleadosService>();
builder.Services.AddSingleton<FacturasService>();
builder.Services.AddSingleton<InventarioService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
