using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () =>
{
    return $"AppApi";

});

app.MapGet("/GetCustomer", (HttpContext context) =>
{
    string header_UserId = String.Empty;
    if (context.Request.Headers.TryGetValue("Header_UserId", out StringValues values))
    {
        header_UserId = values!;
    }
    return $"AppApi - UserId:{header_UserId}";

});

app.Run();

