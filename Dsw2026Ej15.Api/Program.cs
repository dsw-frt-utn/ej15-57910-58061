using Dsw2026Ej15.Api.Middlewares;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
;
var builder = WebApplication.CreateBuilder(args);

var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=Dsw2026Ej16;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True";

builder.Services.AddDbContext<Dsw2026Ej15DbContext>(option =>
{
    option.UseSqlServer(connectionString);
}); 
builder.Services.AddControllers();
builder.Services.AddScoped<IPersistence, PersistenceEf>();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.MapHealthChecks("/health-check");
app.UseAuthorization();
app.MapControllers();
app.Run();
