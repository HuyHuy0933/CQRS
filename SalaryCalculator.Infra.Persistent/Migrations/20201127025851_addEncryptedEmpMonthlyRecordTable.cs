using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SalaryCalculator.Infra.Persistent.Migrations
{
    public partial class addEncryptedEmpMonthlyRecordTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EncryptedEmpMonthlyEnteredRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EncryptedRecord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EncryptedDate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncryptedEmpMonthlyEnteredRecords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EncryptedEmpMonthlyEnteredRecords_EncryptedDate",
                table: "EncryptedEmpMonthlyEnteredRecords",
                column: "EncryptedDate",
                unique: true,
                filter: "[EncryptedDate] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EncryptedEmpMonthlyEnteredRecords");
        }
    }
}
