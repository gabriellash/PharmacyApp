using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharma.Migrations
{
    public partial class NewInitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicine",
                columns: table => new
                {
                    Medicine_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Medicine_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medicine_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medicine_Reg_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medicine_Size = table.Column<double>(type: "float", nullable: false),
                    Medicine_Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medicine_Form = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medicine_Qty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medicine_Price = table.Column<double>(type: "float", nullable: false),
                    Medicine_Expiry_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicine", x => x.Medicine_Id);
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    Request_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Request_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Request_User_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
               
                    Medicine_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Request_Id);
                    table.ForeignKey(
                        name: "FK_Request_Medicine_Medicine_Id",
                        column: x => x.Medicine_Id,
                        principalTable: "Medicine",
                        principalColumn: "Medicine_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Request_Medicine_Id",
                table: "Request",
                column: "Medicine_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "Medicine");
        }
    }
}
