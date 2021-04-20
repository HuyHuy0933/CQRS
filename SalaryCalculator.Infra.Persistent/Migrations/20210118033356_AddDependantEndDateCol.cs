using Microsoft.EntityFrameworkCore.Migrations;

namespace SalaryCalculator.Infra.Persistent.Migrations
{
    public partial class AddDependantEndDateCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfDependants",
                table: "SyncEmployeeProfiles");

            migrationBuilder.AddColumn<string>(
                name: "DependantEndDates",
                table: "SyncEmployeeProfiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DependantEndDates",
                table: "SyncEmployeeProfiles");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfDependants",
                table: "SyncEmployeeProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
