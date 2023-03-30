using UlidAsGuid.Binder;
using UlidAsGuid.DapperSetting;
using UlidAsGuid.SwaggerSetting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);




builder.Services
       .AddControllers(o =>
       {
           o.ModelBinderProviders.Insert(0, new UlidBinderProvider());
       })
       .AddJsonOptions(opts =>
       {
           opts.JsonSerializerOptions.Converters.Add(new GuidUlidJsonConverter());
       })
       ;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.DocumentFilter<UlidDocumentFilter>();
    c.ParameterFilter<GuidParameterFilter>();
});

// setup handler 
Dapper.SqlMapper.AddTypeHandler(new GuidUlidHandler());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();


