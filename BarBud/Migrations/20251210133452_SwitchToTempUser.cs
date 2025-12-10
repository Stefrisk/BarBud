using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarBud.Migrations
{
    /// <inheritdoc />
    public partial class SwitchToTempUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TempUserId",
                table: "Recipes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TempUserID",
                table: "Ingredients",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TempUserID",
                table: "Drinks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TempUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_TempUserId",
                table: "Recipes",
                column: "TempUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_TempUserID",
                table: "Ingredients",
                column: "TempUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_TempUserID",
                table: "Drinks",
                column: "TempUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_TempUsers_TempUserID",
                table: "Drinks",
                column: "TempUserID",
                principalTable: "TempUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_TempUsers_TempUserID",
                table: "Ingredients",
                column: "TempUserID",
                principalTable: "TempUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_TempUsers_TempUserId",
                table: "Recipes",
                column: "TempUserId",
                principalTable: "TempUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_TempUsers_TempUserID",
                table: "Drinks");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_TempUsers_TempUserID",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_TempUsers_TempUserId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "TempUsers");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_TempUserId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_TempUserID",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Drinks_TempUserID",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "TempUserId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "TempUserID",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "TempUserID",
                table: "Drinks");
        }
    }
}
