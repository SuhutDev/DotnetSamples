using DddEf.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DddEf.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");  

        services.AddDbContext<DddEfContext>((sp, options) =>
        {  
            options.UseSqlServer(connectionString); 
        });

        //services.AddScoped<DddEfContext>(provider => provider.GetRequiredService<DddEfContext>());
        services.AddScoped<DddEfContext>();


        return services;
    }
}
