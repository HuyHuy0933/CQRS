using Microsoft.EntityFrameworkCore.Migrations;

namespace SalaryCalculator.Infra.Persistent.Migrations
{
    public partial class addCol_DefaultProbationTaxRate_IsInsurancePaidFullSalary_InsurancePaidAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultTaxRate",
                table: "SalarySettings");

            migrationBuilder.AddColumn<decimal>(
                name: "DefaultProbationTaxRate",
                table: "SalarySettings",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InsurancePaidAmount",
                table: "SalarySettings",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsInsurancePaidFullSalary",
                table: "SalarySettings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultProbationTaxRate",
                table: "SalarySettings");

            migrationBuilder.DropColumn(
                name: "InsurancePaidAmount",
                table: "SalarySettings");

            migrationBuilder.DropColumn(
                name: "IsInsurancePaidFullSalary",
                table: "SalarySettings");

            migrationBuilder.AddColumn<decimal>(
                name: "DefaultTaxRate",
                table: "SalarySettings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
