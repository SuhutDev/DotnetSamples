using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", (HttpContext context) =>
{
    var header_Name = string.Empty;
    if(context.Request.Headers.TryGetValue("Header_Name", out StringValues stringValues))
    {
        header_Name = stringValues.ToString();
    } 
    return $"AccountApi - {header_Name}";
});

app.Run();

