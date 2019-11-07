using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project100.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    registerId = table.Column<int>(nullable: false),
                    firstName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    phoneNumber = table.Column<long>(nullable: false),
                    address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Business",
                columns: table => new
                {
                    accountNumber = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(nullable: true),
                    InterestRate = table.Column<double>(nullable: false),
                    Balance = table.Column<double>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    CustomersId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.accountNumber);
                    table.ForeignKey(
                        name: "FK_Business_Customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Checking",
                columns: table => new
                {
                    accountNumber = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(nullable: true),
                    InterestRate = table.Column<double>(nullable: false),
                    Balance = table.Column<double>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    CustomersId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checking", x => x.accountNumber);
                    table.ForeignKey(
                        name: "FK_Checking_Customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Loan",
                columns: table => new
                {
                    accountNumber = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(nullable: true),
                    InterestRate = table.Column<double>(nullable: false),
                    Balance = table.Column<double>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    CustomersId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan", x => x.accountNumber);
                    table.ForeignKey(
                        name: "FK_Loan_Customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Term",
                columns: table => new
                {
                    accountNumber = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    period = table.Column<int>(nullable: false),
                    type = table.Column<string>(nullable: true),
                    InterestRate = table.Column<double>(nullable: false),
                    Balance = table.Column<double>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    CustomersId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Term", x => x.accountNumber);
                    table.ForeignKey(
                        name: "FK_Term_Customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    accountNumber = table.Column<int>(nullable: false),
                    numberOfMonth = table.Column<int>(nullable: false),
                    accountType = table.Column<string>(nullable: true),
                    amount = table.Column<double>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    type = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    CheckingaccountNumber = table.Column<int>(nullable: true),
                    BusinessaccountNumber = table.Column<int>(nullable: true),
                    LoanaccountNumber = table.Column<int>(nullable: true),
                    TermaccountNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Business_BusinessaccountNumber",
                        column: x => x.BusinessaccountNumber,
                        principalTable: "Business",
                        principalColumn: "accountNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Checking_CheckingaccountNumber",
                        column: x => x.CheckingaccountNumber,
                        principalTable: "Checking",
                        principalColumn: "accountNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_Loan_LoanaccountNumber",
                        column: x => x.LoanaccountNumber,
                        principalTable: "Loan",
                        principalColumn: "accountNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Term_TermaccountNumber",
                        column: x => x.TermaccountNumber,
                        principalTable: "Term",
                        principalColumn: "accountNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Business_CustomersId",
                table: "Business",
                column: "CustomersId");

            migrationBuilder.CreateIndex(
                name: "IX_Checking_CustomersId",
                table: "Checking",
                column: "CustomersId");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_CustomersId",
                table: "Loan",
                column: "CustomersId");

            migrationBuilder.CreateIndex(
                name: "IX_Term_CustomersId",
                table: "Term",
                column: "CustomersId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_BusinessaccountNumber",
                table: "Transaction",
                column: "BusinessaccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CheckingaccountNumber",
                table: "Transaction",
                column: "CheckingaccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CustomerId",
                table: "Transaction",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_LoanaccountNumber",
                table: "Transaction",
                column: "LoanaccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_TermaccountNumber",
                table: "Transaction",
                column: "TermaccountNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Business");

            migrationBuilder.DropTable(
                name: "Checking");

            migrationBuilder.DropTable(
                name: "Loan");

            migrationBuilder.DropTable(
                name: "Term");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
