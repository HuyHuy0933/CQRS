using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SalaryCalculator.Infra.Persistent.Migrations
{
    public partial class addTableEncryptedSalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EncryptedEmpMonthlySalaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EncryptedSalary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EncryptedRecordId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncryptedEmpMonthlySalaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EncryptedEmpMonthlySalaries_EncryptedEmpMonthlyEnteredRecords_EncryptedRecordId",
                        column: x => x.EncryptedRecordId,
                        principalTable: "EncryptedEmpMonthlyEnteredRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EncryptedEmpMonthlySalaries_EncryptedRecordId",
                table: "EncryptedEmpMonthlySalaries",
                column: "EncryptedRecordId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EncryptedEmpMonthlySalaries");
        }
    }
}
