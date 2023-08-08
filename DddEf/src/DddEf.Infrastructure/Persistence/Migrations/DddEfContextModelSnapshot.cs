﻿// <auto-generated />
using System;
using DddEf.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DddEf.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(DddEfContext))]
    partial class DddEfContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DddEf.Domain.Aggregates.Customer.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomerCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("Tm_Customer", (string)null);
                });

            modelBuilder.Entity("DddEf.Domain.Aggregates.Product.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("Tm_Product", (string)null);
                });

            modelBuilder.Entity("DddEf.Domain.Aggregates.SalesOrder.SalesOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.Property<DateTime>("TransDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tx_SalesOrder", (string)null);
                });

            modelBuilder.Entity("DddEf.Domain.Aggregates.SalesOrder.SalesOrder", b =>
                {
                    b.OwnsMany("DddEf.Domain.Aggregates.SalesOrder.Entities.SalesOrderItem", "Items", b1 =>
                        {
                            b1.Property<Guid>("Det1Id")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("Det1Id");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<double?>("Price")
                                .HasColumnType("float");

                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<double?>("Qty")
                                .HasColumnType("float");

                            b1.Property<int>("RowNumber")
                                .HasColumnType("int");

                            b1.Property<double?>("Total")
                                .HasColumnType("float");

                            b1.HasKey("Det1Id");

                            b1.HasIndex("Id", "RowNumber")
                                .IsUnique();

                            b1.ToTable("Tx_SalesOrder_Item", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("Id");
                        });

                    b.OwnsOne("DddEf.Domain.Common.ValueObjects.Address", "BillAddress", b1 =>
                        {
                            b1.Property<Guid>("DetId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("DetId");

                            b1.HasIndex("Id")
                                .IsUnique();

                            b1.ToTable("Tx_SalesOrder__BillAddress", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("Id");
                        });

                    b.OwnsOne("DddEf.Domain.Common.ValueObjects.Address", "ShipAddress", b1 =>
                        {
                            b1.Property<Guid>("SalesOrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.HasKey("SalesOrderId");

                            b1.ToTable("Tx_SalesOrder");

                            b1.WithOwner()
                                .HasForeignKey("SalesOrderId");
                        });

                    b.Navigation("BillAddress")
                        .IsRequired();

                    b.Navigation("Items");

                    b.Navigation("ShipAddress")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
