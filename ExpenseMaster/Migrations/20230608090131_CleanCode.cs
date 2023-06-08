using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseMaster.Migrations
{
    /// <inheritdoc />
    public partial class CleanCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Category_CategoryId1",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Users_UserId1",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Users_UserId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialGoal_Users_UserId",
                table: "FinancialGoal");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_CategoryId1",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_UserId1",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Budgets");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "FinancialGoal",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Budgets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Budgets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_CategoryId",
                table: "Budgets",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_UserId",
                table: "Budgets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Category_CategoryId",
                table: "Budgets",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Users_UserId",
                table: "Budgets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Users_UserId",
                table: "Expenses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialGoal_Users_UserId",
                table: "FinancialGoal",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Category_CategoryId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Users_UserId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Users_UserId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialGoal_Users_UserId",
                table: "FinancialGoal");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_CategoryId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_UserId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Budgets");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "FinancialGoal",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Expenses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "Budgets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Budgets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_CategoryId1",
                table: "Budgets",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_UserId1",
                table: "Budgets",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Category_CategoryId1",
                table: "Budgets",
                column: "CategoryId1",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Users_UserId1",
                table: "Budgets",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Users_UserId",
                table: "Expenses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialGoal_Users_UserId",
                table: "FinancialGoal",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
