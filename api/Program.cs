using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniValidation;

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

app.MapGet("/houses", (IHouseRepository repo) => repo.GetAll())
    .Produces<HouseDto[]>(StatusCodes.Status200OK); //// no need to await the task bc it's handled by the framework.

app.MapGet("/house/{houseId:int}", async (int houseId, IHouseRepository repo) =>
{
    var house = await repo.Get(houseId);
    if (house == null) 
    {
        return Results.Problem($"House with ID {houseId} not found.",
        statusCode: 404);
    }

    return Results.Ok(house);
}).ProducesProblem(404).Produces<HouseDetailDto>(StatusCodes.Status200OK);

app.MapPost("/houses", async ([FromBody]HouseDetailDto dto,
    IHouseRepository repo) =>
{
    if (!MiniValidator.TryValidate(dto, out var errors))
        return Results.ValidationProblem(errors);

    var newHouse = await repo.Add(dto);

    return Results.Created($"/house/{newHouse.Id}", newHouse);
}).ProducesValidationProblem().Produces<HouseDetailDto>(StatusCodes.Status201Created);

app.MapPut("/houses", async ([FromBody] HouseDetailDto dto,
    IHouseRepository repo) =>
{
    if (!MiniValidator.TryValidate(dto, out var errors))
        return Results.ValidationProblem(errors);

    if (await repo.Get(dto.Id) == null) 
    {
        return Results.Problem($"House {dto.Id} not found.",
        statusCode: 404);
    }

    var updatedHouse = await repo.Update(dto);
    return Results.Ok(updatedHouse);
}).ProducesValidationProblem().ProducesProblem(404).Produces<HouseDetailDto>(StatusCodes.Status200OK);

app.MapDelete("/houses/{houseId:int}", async (int houseId,
    IHouseRepository repo) =>
{
    if (await repo.Get(houseId) == null)
    {
        return Results.Problem($"House {houseId} not found.",
        statusCode: 404);
    }

    await repo.Delete(houseId);
    return Results.Ok();
}).ProducesProblem(404).Produces(StatusCodes.Status200OK);

app.Run();