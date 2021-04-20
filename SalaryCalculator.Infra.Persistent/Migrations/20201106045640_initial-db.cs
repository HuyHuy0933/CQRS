using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SalaryCalculator.Infra.Persistent.Migrations
{
    public partial class initialdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalaryCurrencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryCurrencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgressiveTaxRateSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LowerBound = table.Column<decimal>(nullable: false),
                    UpperBound = table.Column<decimal>(nullable: false),
                    Rate = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    CurrencyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressiveTaxRateSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressiveTaxRateSettings_SalaryCurrencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "SalaryCurrencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalarySettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CoefficientSocialCare = table.Column<decimal>(nullable: false),
                    EmployerSocialInsuranceRate = table.Column<decimal>(nullable: false),
                    EmployeeSocialInsuranceRate = table.Column<decimal>(nullable: false),
                    EmployerHealthCareInsuranceRate = table.Column<decimal>(nullable: false),
                    EmployeeHealthCareInsuranceRate = table.Column<decimal>(nullable: false),
                    EmployeeUnemploymentInsuranceRate = table.Column<decimal>(nullable: false),
                    EmployerUnemploymentInsuranceRate = table.Column<decimal>(nullable: false),
                    DefaultTaxRate = table.Column<decimal>(nullable: false),
                    EmployeeUnionFeeRate = table.Column<decimal>(nullable: false),
                    EmployerUnionFeeRate = table.Column<decimal>(nullable: false),
                    CommonMinimumWage = table.Column<decimal>(nullable: false),
                    RegionalMinimumWage = table.Column<decimal>(nullable: false),
                    PersonalDeduction = table.Column<decimal>(nullable: false),
                    DependantDeduction = table.Column<decimal>(nullable: false),
                    MinimumNonWorkingDay = table.Column<int>(nullable: false),
                    CurrencyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalarySettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalarySettings_SalaryCurrencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "SalaryCurrencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgressiveTaxRateSettings_CurrencyId",
                table: "ProgressiveTaxRateSettings",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_SalarySettings_CurrencyId",
                table: "SalarySettings",
                column: "CurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgressiveTaxRateSettings");

            migrationBuilder.DropTable(
                name: "SalarySettings");

            migrationBuilder.DropTable(
                name: "SalaryCurrencies");
        }
    }
}
