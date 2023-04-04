using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ark.IdentityServer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Update__AuditingEntity_props : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                schema: "IdentityServer",
                table: "ApplicationUsers");

            migrationBuilder.RenameColumn(
                name: "DateModified",
                schema: "IdentityServer",
                table: "ApplicationUsers",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "DateDeleted",
                schema: "IdentityServer",
                table: "ApplicationUsers",
                newName: "ModifiedOnUtc");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedOnUtc",
                schema: "IdentityServer",
                table: "ApplicationUsers",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                schema: "IdentityServer",
                table: "ApplicationUsers");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUtc",
                schema: "IdentityServer",
                table: "ApplicationUsers",
                newName: "DateDeleted");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                schema: "IdentityServer",
                table: "ApplicationUsers",
                newName: "DateModified");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateCreated",
                schema: "IdentityServer",
                table: "ApplicationUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
