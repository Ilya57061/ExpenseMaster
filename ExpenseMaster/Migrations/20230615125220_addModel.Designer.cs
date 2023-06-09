﻿// <auto-generated />
using System;
using ExpenseMaster.DAL.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExpenseMaster.Migrations
{
    [DbContext(typeof(ApplicationDatabaseContext))]
    [Migration("20230615125220_addModel")]
    partial class addModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ExpenseMaster.DAL.Models.Budget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<decimal>("Limit")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<decimal>("WarningThreshold")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Budgets", (string)null);
                });

            modelBuilder.Entity("ExpenseMaster.DAL.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Categories", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Category 1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Category 2"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Category 3"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Category 4"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Category 5"
                        });
                });

            modelBuilder.Entity("ExpenseMaster.DAL.Models.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Expenses", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 100.00m,
                            CategoryId = 1,
                            Date = new DateTime(2023, 6, 15, 15, 52, 20, 84, DateTimeKind.Local).AddTicks(2975),
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Amount = 50.00m,
                            CategoryId = 2,
                            Date = new DateTime(2023, 6, 15, 15, 52, 20, 84, DateTimeKind.Local).AddTicks(2987),
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            Amount = 75.00m,
                            CategoryId = 1,
                            Date = new DateTime(2023, 6, 15, 15, 52, 20, 84, DateTimeKind.Local).AddTicks(2988),
                            UserId = 2
                        },
                        new
                        {
                            Id = 4,
                            Amount = 120.00m,
                            CategoryId = 3,
                            Date = new DateTime(2023, 6, 15, 15, 52, 20, 84, DateTimeKind.Local).AddTicks(2989),
                            UserId = 2
                        },
                        new
                        {
                            Id = 5,
                            Amount = 200.00m,
                            CategoryId = 2,
                            Date = new DateTime(2023, 6, 15, 15, 52, 20, 84, DateTimeKind.Local).AddTicks(2990),
                            UserId = 3
                        });
                });

            modelBuilder.Entity("ExpenseMaster.DAL.Models.FinancialGoal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("CurrentAmount")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("GoalName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TargetAmount")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("FinancialGoals", (string)null);
                });

            modelBuilder.Entity("ExpenseMaster.DAL.Models.Income", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Incomes", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 1000.00m,
                            CategoryId = 1,
                            Date = new DateTime(2023, 6, 15, 15, 52, 20, 84, DateTimeKind.Local).AddTicks(3003),
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Amount = 750.00m,
                            CategoryId = 2,
                            Date = new DateTime(2023, 6, 15, 15, 52, 20, 84, DateTimeKind.Local).AddTicks(3004),
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            Amount = 500.00m,
                            CategoryId = 1,
                            Date = new DateTime(2023, 6, 15, 15, 52, 20, 84, DateTimeKind.Local).AddTicks(3005),
                            UserId = 2
                        },
                        new
                        {
                            Id = 4,
                            Amount = 1200.00m,
                            CategoryId = 3,
                            Date = new DateTime(2023, 6, 15, 15, 52, 20, 84, DateTimeKind.Local).AddTicks(3006),
                            UserId = 2
                        },
                        new
                        {
                            Id = 5,
                            Amount = 800.00m,
                            CategoryId = 2,
                            Date = new DateTime(2023, 6, 15, 15, 52, 20, 84, DateTimeKind.Local).AddTicks(3006),
                            UserId = 3
                        });
                });

            modelBuilder.Entity("ExpenseMaster.DAL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("ExpenseMaster.DAL.Models.Budget", b =>
                {
                    b.HasOne("ExpenseMaster.DAL.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExpenseMaster.DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ExpenseMaster.DAL.Models.Expense", b =>
                {
                    b.HasOne("ExpenseMaster.DAL.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExpenseMaster.DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ExpenseMaster.DAL.Models.FinancialGoal", b =>
                {
                    b.HasOne("ExpenseMaster.DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ExpenseMaster.DAL.Models.Income", b =>
                {
                    b.HasOne("ExpenseMaster.DAL.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExpenseMaster.DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
