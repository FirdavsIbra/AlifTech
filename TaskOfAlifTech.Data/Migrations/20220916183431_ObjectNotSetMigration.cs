using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskOfAlifTech.Data.Migrations
{
    public partial class ObjectNotSetMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Attachments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Wallets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DeletedBy",
                table: "Wallets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "Wallets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DeletedBy",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Attachments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DeletedBy",
                table: "Attachments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "Attachments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
