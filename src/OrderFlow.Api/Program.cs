using Microsoft.EntityFrameworkCore;
using OrderFlow.Api.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString=
    builder.Configuration.GetConnectionString("OrderFlow")
    ?? throw new InvalidOperationException("Connection string"
        + " 'OrderFlow' not found.");

builder.Services.AddDbContext<OrderFlowDbContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

app.MapGet("/db-check", async (OrderFlowDbContext context) =>
{
    var canConnect = await context.Database.CanConnectAsync();
    if (canConnect)
        return Results.Ok(new { database = "connected" });
    else
        return Results.StatusCode(503);

});

app.Run();
