using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendIntegrationTest_Ameerhamza.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "todoTable",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    Todo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Completed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true),
                    TodoModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("category_Id", x => x.id);
                    table.ForeignKey(
                        name: "FK_CategoryDetails_todoTable_TodoModelId",
                        column: x => x.TodoModelId,
                        principalTable: "todoTable",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryDetails_TodoModelId",
                table: "CategoryDetails",
                column: "TodoModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryDetails");

            migrationBuilder.DropTable(
                name: "todoTable");
        }
    }
}
