using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Images_UserImageId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Images_UserImageId",
                table: "Patient");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Patient_UserImageId",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Employee_UserImageId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "UserImageId",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "UserImageId",
                table: "Employee");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Patient",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Employee",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Employee");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Patient",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserImageId",
                table: "Patient",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Employee",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserImageId",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

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
    }
}
