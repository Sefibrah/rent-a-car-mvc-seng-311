using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace rent_a_car_mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationsWithRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RenterId = table.Column<int>(type: "int", nullable: true),
                    CarId = table.Column<int>(type: "int", nullable: true),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    Deposit = table.Column<double>(type: "float", nullable: false),
                    FlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservations_Customers_RenterId",
                        column: x => x.RenterId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CarId", "Deposit", "EndDateTime", "EndLocation", "FlightNumber", "RenterId", "StartDateTime", "StartLocation", "TotalAmount" },
                values: new object[,]
                {
                    { 1, 1, 4000.0, new DateTime(2023, 12, 25, 10, 0, 0, 0, DateTimeKind.Unspecified), "Ankara Airport", "TK1002", 1, new DateTime(2023, 12, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), "Ankara Airport", 10000.0 },
                    { 2, 2, 8000.0, new DateTime(2023, 12, 25, 10, 0, 0, 0, DateTimeKind.Unspecified), "Ankara Airport", "TK1005", 2, new DateTime(2023, 12, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), "Ankara Airport", 20000.0 },
                    { 3, 2, 8000.0, new DateTime(2024, 1, 4, 10, 0, 0, 0, DateTimeKind.Unspecified), "Ankara Airport", "TK1008", 2, new DateTime(2023, 12, 30, 10, 0, 0, 0, DateTimeKind.Unspecified), "Ankara Airport", 30000.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CarId",
                table: "Reservations",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RenterId",
                table: "Reservations",
                column: "RenterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
