
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfPkNonClusterSample;


public class MyContext : DbContext
{

    public DbSet<Customer>? Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var builder = modelBuilder.Entity<Customer>();

        builder.HasIndex(a => new { a.SeqId }).IsClustered().IsUnique();
        builder.Property(b => b.SeqId).ValueGeneratedOnAdd().UseIdentityColumn().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        builder.HasKey(a => new { a.Id }).IsClustered(false);

        builder.Property(t => t.CustomerName)
           .HasMaxLength(200);

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // base.OnConfiguring(optionsBuilder);
        string connstring = "server=localhost;Database=MyDb;user id=sa;password=Pass@Word1;TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(connstring);
    }



}