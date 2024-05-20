using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostOfficeAPI.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bags_Shipments_ShipmentId",
                table: "Bags");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_Bags_BagId",
                table: "Parcels");

            migrationBuilder.AddForeignKey(
                name: "FK_Bags_Shipments_ShipmentId",
                table: "Bags",
                column: "ShipmentId",
                principalTable: "Shipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_Bags_BagId",
                table: "Parcels",
                column: "BagId",
                principalTable: "Bags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bags_Shipments_ShipmentId",
                table: "Bags");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_Bags_BagId",
                table: "Parcels");

            migrationBuilder.AddForeignKey(
                name: "FK_Bags_Shipments_ShipmentId",
                table: "Bags",
                column: "ShipmentId",
                principalTable: "Shipments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_Bags_BagId",
                table: "Parcels",
                column: "BagId",
                principalTable: "Bags",
                principalColumn: "Id");
        }
    }
}
