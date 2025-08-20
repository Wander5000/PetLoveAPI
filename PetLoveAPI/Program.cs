using Microsoft.AspNetCore.Mvc;
using PetLoveAPI.Context;
using PetLoveAPI.Services.Auth;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEnd",
        policy => policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddDbContext<PetLoveApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("database")));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("FrontEnd");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
