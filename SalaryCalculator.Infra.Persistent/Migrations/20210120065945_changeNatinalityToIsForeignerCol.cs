using Microsoft.EntityFrameworkCore.Migrations;

namespace SalaryCalculator.Infra.Persistent.Migrations
{
    public partial class changeNatinalityToIsForeignerCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "SyncEmployeeProfiles");

            migrationBuilder.AddColumn<bool>(
                name: "IsForeigner",
                table: "SyncEmployeeProfiles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsForeigner",
                table: "SyncEmployeeProfiles");

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "SyncEmployeeProfiles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
