using DddEf.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DddEf.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddPersistence();
        return services;
    }

    private static void AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<DddEfContext>((serviceProvider, options) =>
        {
            string connstring = "server=localhost;Database=DddEfTest01;user id=sa;password=1234;TrustServerCertificate=True;";
            options.UseSqlServer(connstring,
                b => b.MigrationsAssembly(typeof(DddEfContext).Assembly.FullName)
                );
        });


        services.AddScoped<DddEfContext>(provider => provider.GetRequiredService<DddEfContext>());

    }
}

