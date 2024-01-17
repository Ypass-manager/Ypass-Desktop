using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YpassDesktop.Migrations
{
    /// <inheritdoc />
    public partial class updatesecurityproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "saltCrypt",
                table: "ManagerAccount");

            migrationBuilder.RenameColumn(
                name: "hashPass",
                table: "ManagerAccount",
                newName: "HashPass");

            migrationBuilder.RenameColumn(
                name: "databaseName",
                table: "ManagerAccount",
                newName: "DatabaseName");

            migrationBuilder.RenameColumn(
                name: "accountName",
                table: "ManagerAccount",
                newName: "AccountName");

            migrationBuilder.RenameColumn(
                name: "managerAccountId",
                table: "ManagerAccount",
                newName: "ManagerAccountId");

            migrationBuilder.AddColumn<byte[]>(
                name: "IV",
                table: "ManagerAccount",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "ManagerAccount",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "SaltCritical",
                table: "ManagerAccount",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IV",
                table: "ManagerAccount");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "ManagerAccount");

            migrationBuilder.DropColumn(
                name: "SaltCritical",
                table: "ManagerAccount");

            migrationBuilder.RenameColumn(
                name: "HashPass",
                table: "ManagerAccount",
                newName: "hashPass");

            migrationBuilder.RenameColumn(
                name: "DatabaseName",
                table: "ManagerAccount",
                newName: "databaseName");

            migrationBuilder.RenameColumn(
                name: "AccountName",
                table: "ManagerAccount",
                newName: "accountName");

            migrationBuilder.RenameColumn(
                name: "ManagerAccountId",
                table: "ManagerAccount",
                newName: "managerAccountId");

            migrationBuilder.AddColumn<string>(
                name: "saltCrypt",
                table: "ManagerAccount",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
