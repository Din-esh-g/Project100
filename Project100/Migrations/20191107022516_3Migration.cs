using Microsoft.EntityFrameworkCore.Migrations;

namespace Project100.Migrations
{
    public partial class _3Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Customers_CustomerId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CustomerId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "address",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "phoneNumber",
                table: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Transaction",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Term",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Loan",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Checking",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Business",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CustomerId1",
                table: "Transaction",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Customers_CustomerId1",
                table: "Transaction",
                column: "CustomerId1",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Customers_CustomerId1",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CustomerId1",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Transaction");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Transaction",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Term",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Loan",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "phoneNumber",
                table: "Customers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Checking",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Business",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CustomerId",
                table: "Transaction",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Customers_CustomerId",
                table: "Transaction",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
