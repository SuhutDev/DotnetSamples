var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () =>
{
    // var userId = HttpContext.Items["userId"];
    // var userRole = HttpContext.Items["userRole"];

    return "AppApi";
});

app.Run();

