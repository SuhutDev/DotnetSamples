using DddEf.Domain.Product.Entities;
using DddEf.Domain.Product.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddEf.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{ 
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        ConfigurationProductsTable(builder);
    }

    private void ConfigurationProductsTable(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Tm_Product");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ProductId.Create(value)
                )
                ;

        builder.Property(m => m.ProductCode)
            .HasMaxLength(50);

        builder.Property(m => m.ProductName)
            .HasMaxLength(300);


        //builder.Metadata.FindNavigation(nameof(Product))!
        //    .SetPropertyAccessMode(PropertyAccessMode.Field)
        //    ;

    }
}

