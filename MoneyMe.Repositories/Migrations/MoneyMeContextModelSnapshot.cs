﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoneyMe.Repositories.Data;

#nullable disable

namespace MoneyMe.Repositories.Migrations
{
    [DbContext(typeof(MoneyMeContext))]
    partial class MoneyMeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MoneyMe.Repositories.Data.DBModels.Interest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DurationMax")
                        .HasColumnType("int");

                    b.Property<int>("DurationMin")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("StartFromNMonth")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Interests");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Standard Interest",
                            DurationMax = -1,
                            DurationMin = 0,
                            Name = "Standard",
                            Rate = 0.050000000000000003,
                            StartFromNMonth = 1
                        },
                        new
                        {
                            Id = 2,
                            Description = "First 2 Months Interest Free",
                            DurationMax = -1,
                            DurationMin = 6,
                            Name = "First 2 Months Free",
                            Rate = 0.050000000000000003,
                            StartFromNMonth = 3
                        });
                });

            modelBuilder.Entity("MoneyMe.Repositories.Data.DBModels.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InterestId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InterestId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Interest Free Loan",
                            Name = "ProductA"
                        },
                        new
                        {
                            Id = 2,
                            Description = "First 2 Months Interest Free",
                            InterestId = 2,
                            Name = "ProductB"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Standard Interest",
                            InterestId = 1,
                            Name = "ProductC"
                        });
                });

            modelBuilder.Entity("MoneyMe.Repositories.Data.DBModels.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsApplied")
                        .HasColumnType("bit");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Term")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("MoneyMe.Repositories.Data.DBModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MoneyMe.Repositories.Data.DBModels.Product", b =>
                {
                    b.HasOne("MoneyMe.Repositories.Data.DBModels.Interest", "Interest")
                        .WithMany("Products")
                        .HasForeignKey("InterestId");

                    b.Navigation("Interest");
                });

            modelBuilder.Entity("MoneyMe.Repositories.Data.DBModels.Quote", b =>
                {
                    b.HasOne("MoneyMe.Repositories.Data.DBModels.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.HasOne("MoneyMe.Repositories.Data.DBModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MoneyMe.Repositories.Data.DBModels.Interest", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
