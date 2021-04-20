using Microsoft.EntityFrameworkCore.Migrations;

namespace SalaryCalculator.Infra.Persistent.Migrations
{
    public partial class addMoreForeignInsuranceRateCols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ForeignEmployeeHealthCareInsuranceRate",
                table: "SalarySettings",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ForeignEmployeeSocialInsuranceRate",
                table: "SalarySettings",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ForeignEmployeeUnemploymentInsuranceRate",
                table: "SalarySettings",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ForeignEmployerHealthCareInsuranceRate",
                table: "SalarySettings",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ForeignEmployerSocialInsuranceRate",
                table: "SalarySettings",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ForeignEmployerUnemploymentInsuranceRate",
                table: "SalarySettings",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForeignEmployeeHealthCareInsuranceRate",
                table: "SalarySettings");

            migrationBuilder.DropColumn(
                name: "ForeignEmployeeSocialInsuranceRate",
                table: "SalarySettings");

            migrationBuilder.DropColumn(
                name: "ForeignEmployeeUnemploymentInsuranceRate",
                table: "SalarySettings");

            migrationBuilder.DropColumn(
                name: "ForeignEmployerHealthCareInsuranceRate",
                table: "SalarySettings");

            migrationBuilder.DropColumn(
                name: "ForeignEmployerSocialInsuranceRate",
                table: "SalarySettings");

            migrationBuilder.DropColumn(
                name: "ForeignEmployerUnemploymentInsuranceRate",
                table: "SalarySettings");
        }
    }
}
