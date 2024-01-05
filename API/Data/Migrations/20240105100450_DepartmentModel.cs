using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class DepartmentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "ProductCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDepartments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_DepartmentId",
                table: "ProductCategories",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_ProductDepartments_DepartmentId",
                table: "ProductCategories",
                column: "DepartmentId",
                principalTable: "ProductDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_ProductDepartments_DepartmentId",
                table: "ProductCategories");

            migrationBuilder.DropTable(
                name: "ProductDepartments");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_DepartmentId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "ProductCategories");
        }
    }
}
