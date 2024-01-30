using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class visit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitModel_EmployeeModel_EmployeeId",
                table: "VisitModel");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitModel_PatientModel_PatientId",
                table: "VisitModel");

            migrationBuilder.DropIndex(
                name: "IX_VisitModel_EmployeeId",
                table: "VisitModel");

            migrationBuilder.DropIndex(
                name: "IX_VisitModel_PatientId",
                table: "VisitModel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VisitModel_EmployeeId",
                table: "VisitModel",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitModel_PatientId",
                table: "VisitModel",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitModel_EmployeeModel_EmployeeId",
                table: "VisitModel",
                column: "EmployeeId",
                principalTable: "EmployeeModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitModel_PatientModel_PatientId",
                table: "VisitModel",
                column: "PatientId",
                principalTable: "PatientModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
