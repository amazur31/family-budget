﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;

#nullable disable

namespace Tivix.FamilyBudget.Server.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230921195208_Categories")]
    partial class Categories
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities.BudgetEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities.CategoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BudgetEntityId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BudgetEntityId");

                    b.ToTable("CategoryEntity");
                });

            modelBuilder.Entity("Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities.CategoryEntity", b =>
                {
                    b.HasOne("Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities.BudgetEntity", null)
                        .WithMany("Categories")
                        .HasForeignKey("BudgetEntityId");
                });

            modelBuilder.Entity("Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities.BudgetEntity", b =>
                {
                    b.Navigation("Categories");
                });
#pragma warning restore 612, 618
        }
    }
}
