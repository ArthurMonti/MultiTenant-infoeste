using CursoInfoeste;
using CursoInfoeste.Abstractions.Repositories;
using CursoInfoeste.Abstractions.Services;
using CursoInfoeste.Banco;
using CursoInfoeste.Banco.Repositories;
using CursoInfoeste.Services;
using CursoInfoeste.Swagger;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ITenantRepository, TenantRepository>();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<ICashRegisterRepository, CashRegisterRepository>();
builder.Services.AddScoped<ICashRegisterService, CashRegisterService>();
builder.Services.AddScoped<Persistencia>();
//builder.Services.AddKeyedScoped<ICashRegisterService, CashRegisterService>(1);
//builder.Services.AddKeyedScoped<ICashRegisterService, CashRegisterService>(2);

builder.Services.AddDbContext<CursoInfoesteContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 0))
    )
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<TenantIdHeaderFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<TenantMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
