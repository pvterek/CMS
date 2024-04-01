using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewTableForProfessionProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profession",
                table: "Employee");

            migrationBuilder.AddColumn<int>(
                name: "ProfessionId",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Profession",
                columns: table => new
                {
                    ProfessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profession", x => x.ProfessionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visit_EmployeeId",
                table: "Visit",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_PatientId",
                table: "Visit",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ProfessionId",
                table: "Employee",
                column: "ProfessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Profession_ProfessionId",
                table: "Employee",
                column: "ProfessionId",
                principalTable: "Profession",
                principalColumn: "ProfessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visit_Employee_EmployeeId",
                table: "Visit",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visit_Patient_PatientId",
                table: "Visit",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Profession_ProfessionId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Visit_Employee_EmployeeId",
                table: "Visit");

            migrationBuilder.DropForeignKey(
                name: "FK_Visit_Patient_PatientId",
                table: "Visit");

            migrationBuilder.DropTable(
                name: "Profession");

            migrationBuilder.DropIndex(
                name: "IX_Visit_EmployeeId",
                table: "Visit");

            migrationBuilder.DropIndex(
                name: "IX_Visit_PatientId",
                table: "Visit");

            migrationBuilder.DropIndex(
                name: "IX_Employee_ProfessionId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "ProfessionId",
                table: "Employee");

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
