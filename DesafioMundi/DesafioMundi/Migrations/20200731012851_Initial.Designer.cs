﻿// <auto-generated />
using DesafioMundi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DesafioMundi.Migrations
{
    [DbContext(typeof(MundiContext))]
    [Migration("20200731012851_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DesafioMundi.Entities.Charge", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("Amount");

                    b.Property<string>("Code");

                    b.Property<string>("CreditCardId");

                    b.Property<string>("CustomerId");

                    b.Property<string>("OrderId");

                    b.HasKey("Id");

                    b.HasIndex("CreditCardId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderId")
                        .IsUnique()
                        .HasFilter("[OrderId] IS NOT NULL");

                    b.ToTable("Charges");
                });

            modelBuilder.Entity("DesafioMundi.Entities.CreditCard", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("Brand")
                        .IsRequired();

                    b.Property<string>("CustomerID");

                    b.Property<string>("LestFourNumbers")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CustomerID");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("DesafioMundi.Entities.Customer", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("Document");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Gender");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("DesafioMundi.Entities.Item", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("Amount");

                    b.Property<string>("Description");

                    b.Property<string>("OrderId");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("DesafioMundi.Entities.Order", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ChargeId");

                    b.Property<string>("Code");

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DesafioMundi.Entities.Charge", b =>
                {
                    b.HasOne("DesafioMundi.Entities.CreditCard", "CreditCard")
                        .WithMany("Charges")
                        .HasForeignKey("CreditCardId");

                    b.HasOne("DesafioMundi.Entities.Customer", "Customer")
                        .WithMany("Charges")
                        .HasForeignKey("CustomerId");

                    b.HasOne("DesafioMundi.Entities.Order", "Order")
                        .WithOne("Charge")
                        .HasForeignKey("DesafioMundi.Entities.Charge", "OrderId");
                });

            modelBuilder.Entity("DesafioMundi.Entities.CreditCard", b =>
                {
                    b.HasOne("DesafioMundi.Entities.Customer", "Customer")
                        .WithMany("creditCard")
                        .HasForeignKey("CustomerID");
                });

            modelBuilder.Entity("DesafioMundi.Entities.Item", b =>
                {
                    b.HasOne("DesafioMundi.Entities.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId");
                });
#pragma warning restore 612, 618
        }
    }
}