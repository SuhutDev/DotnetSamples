using ConfigModel;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<SampleSetting>(builder.Configuration.GetSection(SampleSetting.KEY_VALUE));

var app = builder.Build();
app.MapGet("/", (IOptions<SampleSetting> sampleSetting) =>
{
    return $"hello world # Code : {sampleSetting.Value.Code} # Name : {sampleSetting.Value.Name}";
})
;

app.Run();
