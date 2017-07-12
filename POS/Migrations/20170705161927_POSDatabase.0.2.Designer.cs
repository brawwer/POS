using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using POS.Models;

namespace POS.Migrations
{
    [DbContext(typeof(POSDbContext))]
    [Migration("20170705161927_POSDatabase.0.2")]
    partial class POSDatabase02
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("POS.Models.DailySalesEntry", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("AmountCOP");

                    b.Property<int>("DailySalesId");

                    b.Property<int>("InventoryItemId");

                    b.Property<int>("PaymentMethodId");

                    b.Property<int>("Quantity");

                    b.HasKey("ID");

                    b.HasIndex("DailySalesId");

                    b.HasIndex("InventoryItemId");

                    b.HasIndex("PaymentMethodId");

                    b.ToTable("DailySalesEntry");
                });

            modelBuilder.Entity("POS.Models.DailySalesModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("SalesRepId");

                    b.HasKey("ID");

                    b.ToTable("DailySalesModel");
                });

            modelBuilder.Entity("POS.Models.InventoryItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Cost");

                    b.Property<string>("Name");

                    b.Property<double>("PriceCOP");

                    b.Property<double>("PriceUSD");

                    b.Property<string>("Ref");

                    b.Property<int>("ReorderQty");

                    b.Property<int>("StockQty");

                    b.Property<string>("VendorAddress");

                    b.Property<string>("VendorName");

                    b.Property<string>("VendorPhone");

                    b.HasKey("ID");

                    b.ToTable("InventoryItem");
                });

            modelBuilder.Entity("POS.Models.ItemsSoldDaily", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DailySalesId");

                    b.Property<int?>("DailySalesModelID");

                    b.Property<int>("SalesRepId");

                    b.HasKey("ID");

                    b.HasIndex("DailySalesModelID");

                    b.ToTable("ItemsSoldDaily");
                });

            modelBuilder.Entity("POS.Models.PaymentMethod", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Type");

                    b.HasKey("ID");

                    b.ToTable("PaymentMethod");
                });

            modelBuilder.Entity("POS.Models.DailySalesEntry", b =>
                {
                    b.HasOne("POS.Models.DailySalesModel", "DailySales")
                        .WithMany()
                        .HasForeignKey("DailySalesId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("POS.Models.InventoryItem", "InventoryItem")
                        .WithMany()
                        .HasForeignKey("InventoryItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("POS.Models.PaymentMethod", "PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("POS.Models.ItemsSoldDaily", b =>
                {
                    b.HasOne("POS.Models.DailySalesModel", "DailySalesModel")
                        .WithMany("ItemsSoldDaily")
                        .HasForeignKey("DailySalesModelID");
                });
        }
    }
}
