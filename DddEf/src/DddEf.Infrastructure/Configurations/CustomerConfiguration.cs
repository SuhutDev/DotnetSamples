using DddEf.Domain.Customer;
using DddEf.Domain.Customer.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddEf.Infrastructure.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{ 
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        ConfigurationCustomersTable(builder);
    }

    private void ConfigurationCustomersTable(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Tm_Customer");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => CustomerId.Create(value)
                )
                ;

        builder.Property(m => m.CustomerCode)
            .HasMaxLength(50);

        builder.Property(m => m.CustomerName)
            .HasMaxLength(300);


        //builder.Metadata.FindNavigation(nameof(Customer))!
        //    .SetPropertyAccessMode(PropertyAccessMode.Field)
        //    ;

    }
}

