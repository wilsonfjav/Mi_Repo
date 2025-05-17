using Microsoft.EntityFrameworkCore;
using MovimientoEstudiantil.Data;

var builder = WebApplication.CreateBuilder(args);

// Agrega servicios al contenedor

builder.Services.AddControllers(); // Solo controladores (API)

// Configurar EF Core con SQL Server
builder.Services.AddDbContext<MovimientoEstudiantilContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);

// ✅ Agrega cache en memoria requerido por las sesiones
builder.Services.AddDistributedMemoryCache();

// ✅ Agrega soporte para sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware para servir archivos estáticos (ej: Swagger customize.js)
app.UseStaticFiles();

// Middleware para sesiones
app.UseSession();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovimientoEstudiantil API V1");
        c.InjectJavascript("/swagger-custom/customize.js"); // Comentá esta línea si no tenés ese JS
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
