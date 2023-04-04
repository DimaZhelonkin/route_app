using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ark.Routing.Migrations
{
    /// <inheritdoc />
    public partial class Update__OutboxMessage_PublishedOnUtc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedOnUtc",
                schema: "Routing",
                table: "OutboxMessages",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishedOnUtc",
                schema: "Routing",
                table: "OutboxMessages");
        }
    }
}
