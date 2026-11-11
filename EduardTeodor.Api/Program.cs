using EduardTeodor.Api.Endpoints;
using EduardTeodor.Api.Repositories;
using EduardTeodor.Api.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("Database")
    ?? throw new InvalidOperationException("Database connection string is missing.");

builder.Services.AddNpgsqlDataSource(connectionString);
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IAdminPostService, AdminPostService>();
builder.Services.AddScoped<IAdminPostRepository, AdminPostRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


app.MapPostEndpoints();
app.MapAdminPostEndpoints();

app.Run();
