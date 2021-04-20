using Microsoft.EntityFrameworkCore.Migrations;

namespace SalaryCalculator.Infra.Persistent.Migrations
{
    public partial class ChangeRateIntToDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "ProgressiveTaxRateSettings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rate",
                table: "ProgressiveTaxRateSettings",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
