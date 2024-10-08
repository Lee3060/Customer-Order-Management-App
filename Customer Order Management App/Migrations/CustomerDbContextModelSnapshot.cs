﻿// <auto-generated />
using System;
using Customer_Order_Management_App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Customer_Order_Management_App.Migrations
{
    [DbContext(typeof(CustomerDbContext))]
    partial class CustomerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Customer_Order_Management_App.Models.Customer", b =>
                {
                    b.Property<int>("CustmerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustmerId"));

                    b.Property<string>("CustomerAge")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("CustmerId");

                    b.ToTable("Customers");
                });
#pragma warning restore 612, 618
        }
    }
}
