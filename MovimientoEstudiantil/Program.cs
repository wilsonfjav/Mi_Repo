using Microsoft.EntityFrameworkCore;
using MovimientoEstudiantil.Data;

var builder = WebApplication.CreateBuilder(args);

// Registrar DbContext con cadena de conexión y tolerancia a errores transitorios
builder.Services.AddDbContext<MovimientoEstudiantilContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure() // Agrega resiliencia a fallos temporales
    )
);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
