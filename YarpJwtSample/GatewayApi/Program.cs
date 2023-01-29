var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("yarp"))
    ;

var app = builder.Build();


app.MapGet("/", () =>
{
    return "GatewayApi";
});
app.MapReverseProxy();

app.Run();
