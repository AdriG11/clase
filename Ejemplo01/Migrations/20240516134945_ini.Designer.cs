﻿// <auto-generated />
using Ejemplo01.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ejemplo01.Migrations
{
    [DbContext(typeof(BankContext))]
    [Migration("20240516134945_ini")]
    partial class ini
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ejemplo01.Models.AccountTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Credit")
                        .HasColumnType("decimal (18,2)");

                    b.Property<decimal>("Debit")
                        .HasColumnType("decimal (18,2)");

                    b.HasKey("Id");

                    b.ToTable("AccountTransactions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountNumber = "10",
                            Credit = 400m,
                            Debit = 0m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
