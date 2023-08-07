using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

Action<ResourceBuilder> configureResource = r => r.AddService(
    serviceName: "ServiceOpenTelemetryLogs",
    serviceVersion: typeof(Program).Assembly.GetName().Version?.ToString() ?? "unknown",
    serviceInstanceId: Environment.MachineName);

// Clear default logging providers used by WebApplication host.
builder.Logging.ClearProviders();

// Configure OpenTelemetry Logging.
builder.Logging.AddOpenTelemetry(options =>
{
    var resourceBuilder = ResourceBuilder.CreateDefault();
    configureResource(resourceBuilder);
    options.SetResourceBuilder(resourceBuilder);


    options.AddOtlpExporter(otlpOptions =>
    {
        // Use IConfiguration directly for Otlp exporter endpoint option.
        otlpOptions.Endpoint = new Uri("http://localhost:4317");
    });
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


app.MapGet("/cmdSuccess", async (ILogger<Program> logger) =>
{
    var random = new Random();
    int dice = random.Next(0, 5);
    await Task.Delay(dice * 1000);

    logger.LogInformation($"DELAY : {dice} ## cmdSuccess");

    return Results.Ok();
}).WithOpenApi();

app.MapGet("/cmdFail", async (ILogger<Program> logger) =>
{
    var random = new Random();
    int dice = random.Next(0, 5);
    await Task.Delay(dice * 1000);

    logger.LogInformation($"JANCUK : {dice} ## cmdFail");

    return Results.BadRequest();
}).WithOpenApi();


app.Run();

