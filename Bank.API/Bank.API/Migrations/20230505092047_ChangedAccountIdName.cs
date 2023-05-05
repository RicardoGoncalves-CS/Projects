using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAccountIdName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Accounts",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Accounts",
                newName: "AccountId");
        }
    }
}
