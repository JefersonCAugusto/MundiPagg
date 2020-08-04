using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioMundi.Migrations
{
    public partial class UpdateRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charges_Customers_CustomerId",
                table: "Charges");

            migrationBuilder.DropForeignKey(
                name: "FK_Charges_Orders_OrderId",
                table: "Charges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Charges",
                table: "Charges");

            migrationBuilder.DropIndex(
                name: "IX_Charges_OrderId",
                table: "Charges");

            migrationBuilder.AlterColumn<string>(
                name: "OrderId",
                table: "Charges",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Charges",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Charges",
                table: "Charges",
                columns: new[] { "Id", "CustomerId", "OrderId" });

            migrationBuilder.CreateIndex(
                name: "IX_Charges_OrderId",
                table: "Charges",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Charges_Customers_CustomerId",
                table: "Charges",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Charges_Orders_OrderId",
                table: "Charges",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charges_Customers_CustomerId",
                table: "Charges");

            migrationBuilder.DropForeignKey(
                name: "FK_Charges_Orders_OrderId",
                table: "Charges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Charges",
                table: "Charges");

            migrationBuilder.DropIndex(
                name: "IX_Charges_OrderId",
                table: "Charges");

            migrationBuilder.AlterColumn<string>(
                name: "OrderId",
                table: "Charges",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Charges",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Charges",
                table: "Charges",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Charges_OrderId",
                table: "Charges",
                column: "OrderId",
                unique: true,
                filter: "[OrderId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Charges_Customers_CustomerId",
                table: "Charges",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Charges_Orders_OrderId",
                table: "Charges",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
