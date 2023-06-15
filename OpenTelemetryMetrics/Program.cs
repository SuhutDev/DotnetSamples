using System.Reflection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

var assemblyVersion = Assembly.GetExecutingAssembly()
    .GetName().Version?.ToString() ?? "0.0.0";


//ON PROGRESS
builder.Services.AddOpenTelemetry()
  .WithTracing(options => options
        .ConfigureResource(resourceBuilder =>
        {
            resourceBuilder.AddService(
                builder.Environment.ApplicationName,
                builder.Environment.EnvironmentName,
                "1.0",
                false,
                Environment.MachineName);
        })
        .AddAspNetCoreInstrumentation(o =>
        {
            o.Filter = ctx => ctx.Request.Path != "/metrics";
        })
        .AddHttpClientInstrumentation()
        .AddOtlpExporter(options =>
        {
            options.Endpoint = new Uri("http://localhost:4317");
        })
    )
  .WithMetrics(options => options
        .ConfigureResource(resourceBuilder =>
        {
            resourceBuilder.AddService(
                builder.Environment.ApplicationName,
                builder.Environment.EnvironmentName,
                "1.0",
                false,
                Environment.MachineName);
        })
        .AddAspNetCoreInstrumentation(o =>
        {
            o.Filter = (_, ctx) => ctx.Request.Path != "/metrics";
        })
        .AddHttpClientInstrumentation()
        .AddRuntimeInstrumentation()
        .AddProcessInstrumentation()
        .AddOtlpExporter(opts =>
        {
            opts.Endpoint = new Uri("http://localhost:4317");
        })
        //.AddPrometheusExporter()

    )
  ;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//app.UseOpenTelemetryPrometheusScrapingEndpoint();

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

    return Results.Ok();
}).WithOpenApi();

app.MapGet("/cmdFail", async () =>
{
    var random = new Random();
    int dice = random.Next(0, 5);
    await Task.Delay(dice * 1000);  

    return Results.BadRequest();
}).WithOpenApi();


app.Run();
