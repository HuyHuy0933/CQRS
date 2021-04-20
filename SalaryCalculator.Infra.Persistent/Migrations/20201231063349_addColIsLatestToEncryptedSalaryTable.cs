using Microsoft.EntityFrameworkCore.Migrations;

namespace SalaryCalculator.Infra.Persistent.Migrations
{
    public partial class addColIsLatestToEncryptedSalaryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLatest",
                table: "EncryptedEmpMonthlySalaries",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLatest",
                table: "EncryptedEmpMonthlySalaries");
        }
    }
}
