using EduardTeodor.Api.Data;
using EduardTeodor.Api.Models;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using EFCore.NamingConventions;
using EduardTeodor.Api.Endpoints;
using EduardTeodor.Api.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<PostService>();
builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            npgsql => npgsql.MapEnum<PostType>("post_type"))
        .UseSnakeCaseNamingConvention());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

var v1 = app.MapGroup("/v1");
v1.MapPostEndpoints();
app.Run();
