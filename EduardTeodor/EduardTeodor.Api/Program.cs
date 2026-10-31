using EduardTeodor.Api.Data;
using EduardTeodor.Api.Models;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using EFCore.NamingConventions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
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

app.UseHttpsRedirection();



app.Run();
