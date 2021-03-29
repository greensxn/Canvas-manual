using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCanvas.Migrations
{
    public partial class migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    ID_page = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    pageName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.ID_page);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID_role = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID_role);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    ID_user = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_role = table.Column<int>(nullable: false),
                    isConfirmed = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID_user);
                    table.ForeignKey(
                        name: "FK_Users_Roles_ID_role",
                        column: x => x.ID_role,
                        principalTable: "Roles",
                        principalColumn: "ID_role",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Authentication",
                columns: table => new
                {
                    ID_auth = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_user = table.Column<int>(nullable: false),
                    Code = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authentication", x => x.ID_auth);
                    table.ForeignKey(
                        name: "FK_Authentication_Users_ID_user",
                        column: x => x.ID_user,
                        principalTable: "Users",
                        principalColumn: "ID_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    ID_comment = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_page = table.Column<int>(nullable: false),
                    ID_user = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    isReply = table.Column<bool>(nullable: false),
                    ID_reply_comment = table.Column<int>(nullable: false),
                    Commentary = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.ID_comment);
                    table.ForeignKey(
                        name: "FK_Comments_Pages_ID_page",
                        column: x => x.ID_page,
                        principalTable: "Pages",
                        principalColumn: "ID_page",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_ID_user",
                        column: x => x.ID_user,
                        principalTable: "Users",
                        principalColumn: "ID_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    ID_file = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_user = table.Column<int>(nullable: false),
                    Avatar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.ID_file);
                    table.ForeignKey(
                        name: "FK_Files_Users_ID_user",
                        column: x => x.ID_user,
                        principalTable: "Users",
                        principalColumn: "ID_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "ID_page", "pageName" },
                values: new object[,]
                {
                    { 1, "Базовое использование" },
                    { 2, "Рисование фигур с помощью canvas" },
                    { 3, "Стили и цвета" },
                    { 4, "Атрибуты" },
                    { 5, "События" },
                    { 6, "Комментарии" },
                    { 7, "Главная" },
                    { 8, "Вход" },
                    { 9, "Регистрация" },
                    { 10, "Профиль" },
                    { 11, "Редактирование" },
                    { 12, "Создать" },
                    { 13, "Пользователи" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "ID_role", "Name", "Role" },
                values: new object[,]
                {
                    { 3, "ADMIN", 2 },
                    { 2, "EDITOR", 1 },
                    { 1, "USER", 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID_user", "Email", "FirstName", "ID_role", "LastName", "Login", "Password", "isConfirmed" },
                values: new object[] { 1, "dmytro.bohdanov@nure.ua", "Богдан", 3, "Димов", "admin", "adminp", true });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID_user", "Email", "FirstName", "ID_role", "LastName", "Login", "Password", "isConfirmed" },
                values: new object[] { 3, "dmytro.bohdanov@nure.ua", "Bogdan", 2, "Dimoff", "editor", "editorp", true });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID_user", "Email", "FirstName", "ID_role", "LastName", "Login", "Password", "isConfirmed" },
                values: new object[] { 2, "dmytro.bohdanov@nure.ua", "Дима", 1, "Богданов", "user", "userp", true });

            migrationBuilder.CreateIndex(
                name: "IX_Authentication_ID_user",
                table: "Authentication",
                column: "ID_user");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ID_page",
                table: "Comments",
                column: "ID_page");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ID_user",
                table: "Comments",
                column: "ID_user");

            migrationBuilder.CreateIndex(
                name: "IX_Files_ID_user",
                table: "Files",
                column: "ID_user",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ID_role",
                table: "Users",
                column: "ID_role");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authentication");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
