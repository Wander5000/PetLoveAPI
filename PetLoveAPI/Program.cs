using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PetLoveAPI.Context;
using PetLoveAPI.Services.Auth;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

//Configuracion de los controladores para manejar errores de validacion con FluentValidation
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(e => e.Value != null && e.Value.Errors != null && e.Value.Errors.Count > 0)
                .Select(e => new
                {
                    Campo = e.Key,
                    Error = e.Value?.Errors?.FirstOrDefault()?.ErrorMessage ?? "Error desconocido"
                })
                .ToList();
            return new BadRequestObjectResult(errors);
        };
    });

builder.Services.AddScoped<IAuthService, AuthService>();

//Configuracion de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEnd",
        policy => policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

//Autenticacion con JWT (NO TOCAR)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        var config = builder.Configuration;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidAudience = config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
        };
    });

//Autorizacion
builder.Services.AddAuthorization();

//Contexto de la base de datos
builder.Services.AddDbContext<PetLoveApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("database")));

builder.Services.AddEndpointsApiExplorer();

//Validacion Swagger con JWT
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme,
        securityScheme: new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Ingrese el token JWT para acceder a los endpoints protegidos.",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT"
        });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme
            }
        },
        new String[]{}
        }
    });
});

var app = builder.Build();

app.UseCors("FrontEnd");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
