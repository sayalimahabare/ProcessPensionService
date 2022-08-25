﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProcessPensionDetails.Data;

namespace ProcessPensionDetails.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProcessPensionDetails.Models.processDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AccountNumber")
                        .HasColumnType("float");

                    b.Property<string>("AdharNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Allowances")
                        .HasColumnType("int");

                    b.Property<string>("BankName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PAN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublicOrPrivate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("SalaryEarned")
                        .HasColumnType("float");

                    b.Property<string>("SelfOrFamilyPension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("pensionAmount")
                        .HasColumnType("float");

                    b.Property<double>("serviceCharge")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("PensionerDetails");
                });
#pragma warning restore 612, 618
        }
    }
}