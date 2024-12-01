using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<LoadingData>();
builder.Services.AddScoped<SuburbService>(); 
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/property", (SuburbService suburbService) =>
{
    var response = suburbService.getProperty();

    // 404
    if(response == null || !response.Any())
    {
        return Results.NotFound("No Properties found");
    }

    // 200
    return Results.Ok(response);
});


app.MapGet("/suburbs", (SuburbService suburbService) =>
{
    
    var response = suburbService.getSuburbInfo();

    // 404
    if(response == null || !response.Any())
    {
        return Results.NotFound("No Property with this id");
    }

    // 200
    return Results.Ok(response);

});

app.MapGet("/property/{id}", (String id, SuburbService suburbService) =>
{

    // 400
    if(!int.TryParse(id, out int parsedInt))
    {
        return Results.BadRequest("Invalid input value - a numeric value is expected for an Id");
    }
    
    var response = suburbService.getPropertyId(id);

    // 404
    if(response == null)
    {
        return Results.NotFound("No Property with this Id");
    }

    // 200
    return Results.Ok(response);

});

app.Run();
