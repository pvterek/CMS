using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImageTableNameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "ImageDataId",
                table: "Patient",
                newName: "ImageId");

            migrationBuilder.RenameColumn(
                name: "ImageDataId",
                table: "Employee",
                newName: "ImageId");

            migrationBuilder.AddColumn<int>(
                name: "UserImageId",
                table: "Patient",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserImageId",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Patient_UserImageId",
                table: "Patient",
                column: "UserImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_UserImageId",
                table: "Employee",
                column: "UserImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Images_UserImageId",
                table: "Employee",
                column: "UserImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Images_UserImageId",
                table: "Patient",
                column: "UserImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Images_UserImageId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Images_UserImageId",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Patient_UserImageId",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Employee_UserImageId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "UserImageId",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "UserImageId",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Patient",
                newName: "ImageDataId");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Employee",
                newName: "ImageDataId");

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
    }
}
