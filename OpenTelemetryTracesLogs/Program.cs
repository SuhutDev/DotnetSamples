using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Logs;
using OpenTelemetryTracesLogs;

var builder = WebApplication.CreateBuilder(args);

Action<ResourceBuilder> configureResource = r => r.AddService(
      serviceName: builder.Environment.ApplicationName,
      serviceNamespace: builder.Environment.EnvironmentName,
      serviceVersion: typeof(Program).Assembly.GetName().Version?.ToString() ?? "unknown",
      serviceInstanceId: Environment.MachineName
);

//Trances
{
    builder.Services.AddOpenTelemetry()
    .WithTracing(options => options
        .ConfigureResource(configureResource)
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddOtlpExporter(options =>
        {
            options.Endpoint = new Uri("http://localhost:4317");
        })
    );
} 

//Logs  
{
    var resourceBuilder = ResourceBuilder.CreateDefault();
    configureResource(resourceBuilder);

    builder.Services.AddLogging((loggingBuilder) => loggingBuilder
            .SetMinimumLevel(LogLevel.Debug)
            .AddOpenTelemetry(options =>
                options.AddConsoleExporter()
                    .SetResourceBuilder(resourceBuilder)
                    .AddProcessor(new ActivityEventLogProcessor())
                    .IncludeScopes = true))
        ;
}


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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


    logger.LogInformation($"JANCUK : {dice} ## cmdSuccess");

    return Results.BadRequest();
}).WithOpenApi();

app.Run();


