using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuthorUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_AuthorPicture_AuthorPictureId",
                table: "Authors");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorPictureId",
                table: "Authors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_AuthorPicture_AuthorPictureId",
                table: "Authors",
                column: "AuthorPictureId",
                principalTable: "AuthorPicture",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_AuthorPicture_AuthorPictureId",
                table: "Authors");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorPictureId",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_AuthorPicture_AuthorPictureId",
                table: "Authors",
                column: "AuthorPictureId",
                principalTable: "AuthorPicture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
