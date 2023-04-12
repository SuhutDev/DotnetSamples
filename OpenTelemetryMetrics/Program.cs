using System.Reflection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;



var builder = WebApplication.CreateBuilder(args);

var assemblyVersion = Assembly.GetExecutingAssembly()
    .GetName().Version?.ToString() ?? "0.0.0";


//ON PROGRESS
builder.Services.AddOpenTelemetry()
  .WithMetrics(options => options
        .ConfigureResource(resourceBuilder =>
        {
            resourceBuilder.AddService(
                "ServiceB",
                serviceVersion: assemblyVersion,
                serviceInstanceId: Environment.MachineName
                );
        })
        .AddHttpClientInstrumentation()
        .AddAspNetCoreInstrumentation() 
        .AddEventCountersInstrumentation(options =>
        {
            options.RefreshIntervalSecs = 1;
            //options.AddEventSources("MyEventSource");
        })
        .AddOtlpExporter(opts =>
        {
            opts.Endpoint = new Uri("http://localhost:4317");
        })

    )
  ;

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
