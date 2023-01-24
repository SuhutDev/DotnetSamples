
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient("Github", client =>
{
    client.BaseAddress = new Uri("https://github.com/SuhutDev");
}).SetHandlerLifetime(TimeSpan.FromMinutes(5));

builder.Services.AddHttpClient("Twitter", client =>
{
    client.BaseAddress = new Uri("https://twitter.com/suhutwadiyo");
}).SetHandlerLifetime(TimeSpan.FromMinutes(5));

var app = builder.Build();

app.MapGet("/", async (IHttpClientFactory clientFactory) =>
{
    var clientGithub = clientFactory.CreateClient("Github");
    var githubRes = await clientGithub.GetStringAsync("/");

    var clientTwitter = clientFactory.CreateClient("Twitter");
    var twitterRes = await clientTwitter.GetStringAsync("/");

    return "hello world ";
})
;

app.Run();

