﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SalaryCalculator.Infra.Persistent;

namespace SalaryCalculator.Infra.Persistent.Migrations
{
    [DbContext(typeof(SalaryCalculatorDbContext))]
    [Migration("20201116035037_addCol_DefaultProbationTaxRate_IsInsurancePaidFullSalary_InsurancePaidAmount")]
    partial class addCol_DefaultProbationTaxRate_IsInsurancePaidFullSalary_InsurancePaidAmount
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SalaryCalculator.Infra.Persistent.Entities.ProgressiveTaxRateSetting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("LowerBound")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TaxRateLevel")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("UpperBound")
                        .HasColumnType("decimal(34,2)");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.ToTable("ProgressiveTaxRateSettings");
                });

            modelBuilder.Entity("SalaryCalculator.Infra.Persistent.Entities.SalaryCurrency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SalaryCurrencies");
                });

            modelBuilder.Entity("SalaryCalculator.Infra.Persistent.Entities.SalarySetting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("CoefficientSocialCare")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CommonMinimumWage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("DefaultProbationTaxRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DependantDeduction")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("EmployeeHealthCareInsuranceRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("EmployeeSocialInsuranceRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("EmployeeUnemploymentInsuranceRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("EmployeeUnionFeeRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("EmployerHealthCareInsuranceRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("EmployerSocialInsuranceRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("EmployerUnemploymentInsuranceRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("EmployerUnionFeeRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("InsurancePaidAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsInsurancePaidFullSalary")
                        .HasColumnType("bit");

                    b.Property<decimal>("MaximumUnionFeeRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MinimumNonWorkingDay")
                        .HasColumnType("int");

                    b.Property<decimal>("PersonalDeduction")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("RegionalMinimumWage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.ToTable("SalarySettings");
                });

            modelBuilder.Entity("SalaryCalculator.Infra.Persistent.Entities.ProgressiveTaxRateSetting", b =>
                {
                    b.HasOne("SalaryCalculator.Infra.Persistent.Entities.SalaryCurrency", "SalaryCurrency")
                        .WithMany("ProgressiveTaxRateSettings")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SalaryCalculator.Infra.Persistent.Entities.SalarySetting", b =>
                {
                    b.HasOne("SalaryCalculator.Infra.Persistent.Entities.SalaryCurrency", "SalaryCurrency")
                        .WithMany("SalarySettings")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
