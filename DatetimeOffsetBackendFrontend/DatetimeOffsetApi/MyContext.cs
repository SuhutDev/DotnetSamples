
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatetimeOffsetApi;


public class MyContext : DbContext
{

    public DbSet<Customer>? Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var builder = modelBuilder.Entity<Customer>();

        builder.HasKey(a => new { a.Id });

        builder.Property(t => t.CustomerName)
           .HasMaxLength(200);

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // base.OnConfiguring(optionsBuilder);
        string connstring = "server=localhost;Database=MyDbDatetimeOffset;user id=sa;password=Pass@Word1;TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(connstring);
    }



}