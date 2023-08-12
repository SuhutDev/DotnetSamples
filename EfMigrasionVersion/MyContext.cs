
using Microsoft.EntityFrameworkCore;

namespace EfMigrasionVersion;


public class MyContext : DbContext
{

    public DbSet<Customer>? Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var builder = modelBuilder.Entity<Customer>();

        builder.HasKey(a => new { a.Id });

        builder.Property(t => t.CustomerCode)
           .HasMaxLength(200);

        builder.Property(t => t.CustomerName)
           .HasMaxLength(200); 

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // base.OnConfiguring(optionsBuilder);
        string connstring = "server=.;Database=EfMigrasionVersion;user id=sa;password=1234;TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(connstring);
    }



}