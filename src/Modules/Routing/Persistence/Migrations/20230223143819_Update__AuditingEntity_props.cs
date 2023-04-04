using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ark.Routing.Migrations
{
    /// <inheritdoc />
    public partial class Update__AuditingEntity_props : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                schema: "Routing",
                table: "Routes");

            migrationBuilder.RenameColumn(
                name: "DateModified",
                schema: "Routing",
                table: "Routes",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "DateDeleted",
                schema: "Routing",
                table: "Routes",
                newName: "ModifiedOnUtc");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedOnUtc",
                schema: "Routing",
                table: "Routes",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                schema: "Routing",
                table: "Routes");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUtc",
                schema: "Routing",
                table: "Routes",
                newName: "DateDeleted");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                schema: "Routing",
                table: "Routes",
                newName: "DateModified");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateCreated",
                schema: "Routing",
                table: "Routes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
