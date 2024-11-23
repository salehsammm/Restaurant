using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Migrations
{
    /// <inheritdoc />
    public partial class MigAddingUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_DetailId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_DetailId",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "OrderDetailId",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "DetailId",
                table: "OrderDetails",
                newName: "OrderId");

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: " با جو درست میشود");

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: " کباب کوبیده");

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description",
                value: "کیکی که در آن شکلات وجود دارد");

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Description", "ImageName", "Name", "Price", "Quantity", "TypeId" },
                values: new object[] { 8, "جوجه به همراه دورچین", "JoojeNoBone", "جوجه کباب بدون استخوان", 320m, 20, 2 });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_FoodId",
                table: "OrderDetails",
                column: "FoodId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_FoodId",
                table: "OrderDetails");

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "OrderDetailId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderDetails",
                newName: "DetailId");

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "سوپ جو با جو درست میشود");

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "چلو کباب کوبیده");

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description",
                value: "کیکی که در آن شکلا وجود دارد");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_DetailId",
                table: "OrderDetails",
                column: "DetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_DetailId",
                table: "OrderDetails",
                column: "DetailId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
