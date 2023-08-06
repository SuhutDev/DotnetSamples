using DddEf.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
//using Microsoft.VisualStudio.TestPlatform.TestHost; 
using System.Data.Common; 

namespace Application.FunctionalTests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly DbConnection _connection;

    public CustomWebApplicationFactory(DbConnection connection)
    {
        _connection = connection;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        { 
            services
                .RemoveAll<DbContextOptions<DddEfContext>>()
                .AddDbContext<DddEfContext>((sp, options) =>
                {  
                    options.UseSqlServer(_connection); 
                });
        });
    }
}
