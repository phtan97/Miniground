using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniGround.API.Migrations
{
    public partial class initialdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TableFootBallFields",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    IsActived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableFootBallFields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableMatchInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FootballFieldId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    MatchCode = table.Column<string>(maxLength: 50, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableMatchInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableSaleAgents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ReferalBounus = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableSaleAgents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableUserBanks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(maxLength: 256, nullable: true),
                    BankName = table.Column<string>(maxLength: 100, nullable: true),
                    Password = table.Column<string>(maxLength: 2147483647, nullable: true),
                    BankNumber = table.Column<string>(maxLength: 50, nullable: true),
                    AccountBalance = table.Column<decimal>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableUserBanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(nullable: true),
                    Username = table.Column<string>(maxLength: 100, nullable: true),
                    Password = table.Column<string>(maxLength: 2147483647, nullable: true),
                    FullName = table.Column<string>(maxLength: 500, nullable: true),
                    IsActived = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Phone = table.Column<string>(maxLength: 100, nullable: true),
                    Role = table.Column<int>(nullable: false),
                    ReferalCode = table.Column<string>(maxLength: 100, nullable: true),
                    NickName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableFieldPrice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFootballField = table.Column<int>(nullable: false),
                    StartDate = table.Column<TimeSpan>(nullable: false),
                    EndDate = table.Column<TimeSpan>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    TableFootBallFieldId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableFieldPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableFieldPrice_TableFootBallFields_TableFootBallFieldId",
                        column: x => x.TableFootBallFieldId,
                        principalTable: "TableFootBallFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableFieldPrice_TableFootBallFieldId",
                table: "TableFieldPrice",
                column: "TableFootBallFieldId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TableFieldPrice");

            migrationBuilder.DropTable(
                name: "TableMatchInfos");

            migrationBuilder.DropTable(
                name: "TableSaleAgents");

            migrationBuilder.DropTable(
                name: "TableUserBanks");

            migrationBuilder.DropTable(
                name: "TableUsers");

            migrationBuilder.DropTable(
                name: "TableFootBallFields");
        }
    }
}
