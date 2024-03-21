using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationToImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageDataId",
                table: "Patient",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageDataId",
                table: "Employee",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patient_ImageDataId",
                table: "Patient",
                column: "ImageDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ImageDataId",
                table: "Employee",
                column: "ImageDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Images_ImageDataId",
                table: "Employee",
                column: "ImageDataId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Images_ImageDataId",
                table: "Patient",
                column: "ImageDataId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Images_ImageDataId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Images_ImageDataId",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Patient_ImageDataId",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Employee_ImageDataId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "ImageDataId",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "ImageDataId",
                table: "Employee");
        }
    }
}
