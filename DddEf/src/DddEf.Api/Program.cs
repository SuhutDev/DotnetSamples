
using DddEf.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddApplicationServices();
//builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddInfrastructure();
var app = builder.Build(); 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//public partial class Program { }
