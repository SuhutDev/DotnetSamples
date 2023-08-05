using DddEf.Domain.Aggregates.Customer;
using DddEf.Domain.Aggregates.Product;
using DddEf.Domain.Aggregates.SalesOrder;
using DddEf.Domain.Aggregates.SalesOrder.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DddEf.Infrastructure.Persistence;

public class DddEfContext : DbContext
{
    public DddEfContext(DbContextOptions<DddEfContext> option) : base(option)
    {
    }
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<SalesOrder> SalesOrders { get; set; } = null!;
    public DbSet<SalesOrderItem> SalesOrderItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 

        base.OnModelCreating(modelBuilder);
    } 
}
 
