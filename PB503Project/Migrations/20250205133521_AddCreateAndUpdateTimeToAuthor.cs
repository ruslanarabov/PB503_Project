using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PB503Project.Migrations
{
    /// <inheritdoc />
    public partial class AddCreateAndUpdateTimeToAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Borrowers_BorrowerId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_LoanItems_BookId",
                table: "LoanItems");

            migrationBuilder.AlterColumn<int>(
                name: "BorrowerId",
                table: "Loans",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "Borrowers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Authors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "Authors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_LoanItems_BookId",
                table: "LoanItems",
                column: "BookId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Borrowers_BorrowerId",
                table: "Loans",
                column: "BorrowerId",
                principalTable: "Borrowers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Borrowers_BorrowerId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_LoanItems_BookId",
                table: "LoanItems");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "Borrowers");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "Authors");

            migrationBuilder.AlterColumn<int>(
                name: "BorrowerId",
                table: "Loans",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_LoanItems_BookId",
                table: "LoanItems",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Borrowers_BorrowerId",
                table: "Loans",
                column: "BorrowerId",
                principalTable: "Borrowers",
                principalColumn: "Id");
        }
    }
}
