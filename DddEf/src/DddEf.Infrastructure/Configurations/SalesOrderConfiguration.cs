using DddEf.Domain.Customer.ValueObjects;
using DddEf.Domain.Product.ValueObjects;
using DddEf.Domain.SalesOrder;
using DddEf.Domain.SalesOrder.Entities;
using DddEf.Domain.SalesOrder.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddEf.Infrastructure.Configurations;

public class SalesOrderConfiguration : IEntityTypeConfiguration<SalesOrder>
{
    public void Configure(EntityTypeBuilder<SalesOrder> builder)
    {
        ConfigurationSalesOrdersTable(builder);
        ConfigurationSalesOrderItemsTable(builder);
    }

    private void ConfigurationSalesOrdersTable(EntityTypeBuilder<SalesOrder> builder)
    {
        builder.ToTable("Tx_SalesOrder");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => SalesOrderId.Create(value)
                )
                ;


        builder.Property(m => m.CustomerId)
           .HasConversion(
               id => id.Value,
               value => CustomerId.Create(value)
               )
               ;

        builder.Property(m => m.TransNo)
            .HasMaxLength(50);

    }


    private void ConfigurationSalesOrderItemsTable(EntityTypeBuilder<SalesOrder> builder)
    {
        builder.OwnsMany(m => m.Items, sb =>
        {
            sb.ToTable("Tx_SalesOrder_Item");

            sb.WithOwner().HasForeignKey(nameof(SalesOrderId));
            sb.Property(nameof(SalesOrderId))
                .HasColumnName(nameof(SalesOrder.Id));

            sb.HasKey(nameof(SalesOrder.Id), nameof(SalesOrderItem.RowNumber)); 
            //sb.HasKey(nameof(SalesOrderItem.Det1Id));

            sb.Property(sb => sb.Det1Id)
                .HasColumnName(nameof(SalesOrderItem.Det1Id)) 
                .ValueGeneratedNever()
                .HasConversion(
                    det1Id => det1Id.Value,
                    value => SalesOrderItemDet1Id.Create(value)
                    );

            sb.Property(m => m.ProductId)
               .HasConversion(
                   productId => productId.Value,
                   value => ProductId.Create(value)
                   )
                   ;
             
        }); 

        builder.Navigation(s => s.Items).Metadata.SetField("_items"); 
        builder.Navigation(s => s.Items).UsePropertyAccessMode(PropertyAccessMode.Field); 

    }

}

