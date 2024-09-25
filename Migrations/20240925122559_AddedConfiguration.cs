using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetailApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price" },
                values: new object[] { 1, "Features:\r\n- Features wraparound prints\r\n- Top rack dishwasher safe\r\n- Insulated stainless steel with removable lid\r\n- Mug holds 15oz(443ml)", true, "Travel Mug", 11.0 });

            migrationBuilder.InsertData(
                table: "ProductApprovalQueues",
                columns: new[] { "Id", "IsApproved", "ProductId", "Reason", "RequestDate" },
                values: new object[] { 2, false, 1, "price is > 10000", new DateTime(2024, 9, 25, 8, 25, 59, 92, DateTimeKind.Local).AddTicks(120) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductApprovalQueues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
