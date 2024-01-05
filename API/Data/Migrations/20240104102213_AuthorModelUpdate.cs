using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuthorModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Authors",
                newName: "Bio");

            migrationBuilder.AddColumn<int>(
                name: "AuthorPictureId",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AuthorPicture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorPicture", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_AuthorPictureId",
                table: "Authors",
                column: "AuthorPictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_AuthorPicture_AuthorPictureId",
                table: "Authors",
                column: "AuthorPictureId",
                principalTable: "AuthorPicture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_AuthorPicture_AuthorPictureId",
                table: "Authors");

            migrationBuilder.DropTable(
                name: "AuthorPicture");

            migrationBuilder.DropIndex(
                name: "IX_Authors_AuthorPictureId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "AuthorPictureId",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "Authors",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
