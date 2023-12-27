using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace rent_a_car_mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "DateOfBirth", "Email", "Name", "Telephone" },
                values: new object[,]
                {
                    { 1, new DateTime(2002, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "seferibrahim2@gmail.com", "Ibrahim Sefer", "+905363344840" },
                    { 2, new DateTime(2001, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "johnsmith@gmail.com", "John Smith", "+905361123440" },
                    { 3, new DateTime(2000, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "markmarkovich@gmail.com", "Mark Markovich", "+905369977660" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
