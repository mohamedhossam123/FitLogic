using BackEnd.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApiProject.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("https://localhost:5001");

builder.Services.AddControllers();
builder.Services.AddScoped<ICaloriesService, CaloriesService>();
builder.Services.AddScoped<IWorkoutService, WorkoutService>();
builder.Services.AddDbContext<FitLogicContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials());
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();
