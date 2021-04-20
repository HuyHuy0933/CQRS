using Microsoft.EntityFrameworkCore.Migrations;

namespace SalaryCalculator.Infra.Persistent.Migrations
{
    public partial class renameTaxRateLevelCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "ProgressiveTaxRateSettings");

            migrationBuilder.AddColumn<int>(
                name: "TaxRateLevel",
                table: "ProgressiveTaxRateSettings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxRateLevel",
                table: "ProgressiveTaxRateSettings");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "ProgressiveTaxRateSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
