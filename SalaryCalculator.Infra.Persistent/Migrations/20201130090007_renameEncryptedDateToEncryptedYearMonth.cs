using Microsoft.EntityFrameworkCore.Migrations;

namespace SalaryCalculator.Infra.Persistent.Migrations
{
    public partial class renameEncryptedDateToEncryptedYearMonth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EncryptedEmpMonthlyEnteredRecords_EncryptedDate",
                table: "EncryptedEmpMonthlyEnteredRecords");

            migrationBuilder.DropColumn(
                name: "EncryptedDate",
                table: "EncryptedEmpMonthlyEnteredRecords");

            migrationBuilder.AddColumn<string>(
                name: "EncryptedYearMonth",
                table: "EncryptedEmpMonthlyEnteredRecords",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EncryptedEmpMonthlyEnteredRecords_EncryptedYearMonth",
                table: "EncryptedEmpMonthlyEnteredRecords",
                column: "EncryptedYearMonth",
                unique: true,
                filter: "[EncryptedYearMonth] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EncryptedEmpMonthlyEnteredRecords_EncryptedYearMonth",
                table: "EncryptedEmpMonthlyEnteredRecords");

            migrationBuilder.DropColumn(
                name: "EncryptedYearMonth",
                table: "EncryptedEmpMonthlyEnteredRecords");

            migrationBuilder.AddColumn<string>(
                name: "EncryptedDate",
                table: "EncryptedEmpMonthlyEnteredRecords",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EncryptedEmpMonthlyEnteredRecords_EncryptedDate",
                table: "EncryptedEmpMonthlyEnteredRecords",
                column: "EncryptedDate",
                unique: true,
                filter: "[EncryptedDate] IS NOT NULL");
        }
    }
}
