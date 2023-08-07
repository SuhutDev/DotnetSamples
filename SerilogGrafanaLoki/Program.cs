
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Grafana.Loki; 

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseSerilog((context, config) =>
    {

        config.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
            .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName) 
            .WriteTo.GrafanaLoki("http://localhost:3100")
            ;

        if (context.HostingEnvironment.IsDevelopment())
            config.WriteTo.Console(new RenderedCompactJsonFormatter());
    });
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} 
 

app.MapGet("/cmdSuccess", async () =>
{
    var random = new Random();
    int dice = random.Next(0, 5);
    await Task.Delay(dice * 1000);

    Log.Information($"DELAY : {dice} ## cmdSuccess");

    return Results.Ok();
}).WithOpenApi();

app.MapGet("/cmdFail", async () =>
{
    var random = new Random();
    int dice = random.Next(0, 5);
    await Task.Delay(dice * 1000);

    Log.Information($"DELAY : {dice} ## cmdFail");

    return Results.BadRequest();
}).WithOpenApi();

 
app.Run();

 