using Microsoft.EntityFrameworkCore.Migrations;

namespace SalaryCalculator.Infra.Persistent.Migrations
{
    public partial class ChangeUpperBoundColType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "UpperBound",
                table: "ProgressiveTaxRateSettings",
                type: "decimal(34,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 34);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "UpperBound",
                table: "ProgressiveTaxRateSettings",
                type: "decimal(18,2)",
                maxLength: 34,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(34,2)");
        }
    }
}
