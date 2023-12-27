using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace rent_a_car_mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddContractsWithRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RenterId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    Deposit = table.Column<double>(type: "float", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contracts_Customers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_Customers_RenterId",
                        column: x => x.RenterId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Contracts",
                columns: new[] { "Id", "CarId", "CustomerId", "Deposit", "DriverId", "EndDateTime", "EndLocation", "RenterId", "StartDateTime", "StartLocation", "TotalAmount" },
                values: new object[,]
                {
                    { 1, 1, null, 4000.0, 1, new DateTime(2023, 12, 25, 10, 0, 0, 0, DateTimeKind.Unspecified), "Ankara Airport", 1, new DateTime(2023, 12, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), "Ankara Airport", 60000.0 },
                    { 2, 2, null, 8000.0, 1, new DateTime(2023, 12, 25, 10, 0, 0, 0, DateTimeKind.Unspecified), "Ankara Airport", 2, new DateTime(2023, 12, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), "Ankara Airport", 50000.0 },
                    { 3, 2, null, 8000.0, 2, new DateTime(2024, 1, 4, 10, 0, 0, 0, DateTimeKind.Unspecified), "Ankara Airport", 2, new DateTime(2023, 12, 30, 10, 0, 0, 0, DateTimeKind.Unspecified), "Ankara Airport", 50000.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CarId",
                table: "Contracts",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CustomerId",
                table: "Contracts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_DriverId",
                table: "Contracts",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_RenterId",
                table: "Contracts",
                column: "RenterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");
        }
    }
}
