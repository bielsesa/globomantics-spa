using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext<HouseDbContext>(o => //// this registers it using the "scoped" scope, which means that a new instance will be created for each request that the API receives.
    o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)); //// that is why the tracking is turned off, bc there will be a new instance on each call.
builder.Services.AddScoped<IHouseRepository, HouseRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(p => p.WithOrigins("http://localhost:3000")
    .AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();

app.MapGet("/houses", (IHouseRepository repo) => repo.GetAll()); //// no need to await the task bc it's handled by the framework.

app.Run();