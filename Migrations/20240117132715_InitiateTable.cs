using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YpassDesktop.Migrations
{
    /// <inheritdoc />
    public partial class InitiateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    accountId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    username = table.Column<string>(type: "TEXT", nullable: false),
                    password = table.Column<string>(type: "TEXT", nullable: false),
                    lastModification = table.Column<DateTime>(type: "TEXT", nullable: false),
                    isFavorite = table.Column<bool>(type: "INTEGER", nullable: false),
                    websiteUrl = table.Column<string>(type: "TEXT", nullable: false),
                    websiteName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.accountId);
                });

            migrationBuilder.CreateTable(
                name: "HistoryConnection",
                columns: table => new
                {
                    historyConnectionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    connectionDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryConnection", x => x.historyConnectionId);
                });

            migrationBuilder.CreateTable(
                name: "ManagerAccount",
                columns: table => new
                {
                    managerAccountId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    accountName = table.Column<string>(type: "TEXT", nullable: false),
                    saltCrypt = table.Column<string>(type: "TEXT", nullable: false),
                    hashPass = table.Column<string>(type: "TEXT", nullable: false),
                    databaseName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerAccount", x => x.managerAccountId);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    settingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    policiesChangePassDays = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.settingId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "HistoryConnection");

            migrationBuilder.DropTable(
                name: "ManagerAccount");

            migrationBuilder.DropTable(
                name: "Setting");
        }
    }
}
