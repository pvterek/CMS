using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class visitedit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitModel_EmployeeModel_AddedById",
                table: "VisitModel");

            migrationBuilder.DropIndex(
                name: "IX_VisitModel_AddedById",
                table: "VisitModel");

            migrationBuilder.DropColumn(
                name: "AddedById",
                table: "VisitModel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddedById",
                table: "VisitModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VisitModel_AddedById",
                table: "VisitModel",
                column: "AddedById");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitModel_EmployeeModel_AddedById",
                table: "VisitModel",
                column: "AddedById",
                principalTable: "EmployeeModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
