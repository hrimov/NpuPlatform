using Microsoft.EntityFrameworkCore;
using NpuBackend.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<NpuDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

// Add other services
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/healthcheck", () =>
    {
        return Results.Json(new { message = "ok" }, statusCode: 200);
    })
.WithName("GetHealthCheck")
.WithOpenApi();


app.MapGet("/database-healthcheck", async (NpuDbContext dbContext) =>
    {
        try
        {
            await dbContext.Database.CanConnectAsync();
            return Results.Json(new { message = "Database is reachable" }, statusCode: 200);
        }
        catch (Exception ex)
        {
            // Return error message if connection fails
            return Results.Json(new { message = "Database is not reachable", error = ex.Message }, statusCode: 500);
        }
    }).WithName("GetDatabaseHealthCheck")
    .WithOpenApi();

app.Run();
