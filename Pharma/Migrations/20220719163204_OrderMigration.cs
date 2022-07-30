using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharma.Migrations
{
    public partial class OrderMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Order_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Order_User_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order_UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order_Medicine_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Order_Id);
                    table.ForeignKey(
                        name: "FK_Order_Medicine_Order_Medicine_Id",
                        column: x => x.Order_Medicine_Id,
                        principalTable: "Medicine",
                        principalColumn: "Medicine_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_Order_Medicine_Id",
                table: "Order",
                column: "Order_Medicine_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.AddColumn<int>(
                name: "Request_Medicine_Id",
                table: "Request",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
