using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Api.Custom;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Core.Interfaces;
using Core.Services;
using Infrastructure.Repositories;
using Application.SQLContext.Usuario.Queries;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(Assembly.Load("Application"));

// Registrar MediatR
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IUsuarioServices, UsuarioServices>();
builder.Services.AddTransient<ITarjetaVirtualRepository, TarjetaVirtualRepository>();
builder.Services.AddTransient<ITarjetaVirtualServices, TarjetaVirtualServices>();
builder.Services.AddTransient<IPasajeServices, PasajeService>();
builder.Services.AddTransient<IPasajeRepository, PasajeRepository>();
builder.Services.AddTransient<IPagoRepository, PagoRepository>();
builder.Services.AddTransient<IPagoServices, PagoService>();


builder.Services.AddDbContext<PruebaDesarrolladorContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL"));
});

builder.Services.AddSingleton<Utilidades>();

builder.Services.AddAuthentication(config => {
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]!))
    };
});

// Configuramos los Cors para q nos deje ejecutar los End-Point desde otro servidor
builder.Services.AddCors(options =>
{
    options.AddPolicy("NewPolicy", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Utilizamos los Cors
app.UseCors("NewPolicy");

// Utilizamos la autenticacion
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
