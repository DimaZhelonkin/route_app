using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ark.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConsumerkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OutboxMessageConsumers",
                table: "OutboxMessageConsumers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OutboxMessageConsumers",
                table: "OutboxMessageConsumers",
                columns: new[] { "Id", "Name" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OutboxMessageConsumers",
                table: "OutboxMessageConsumers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OutboxMessageConsumers",
                table: "OutboxMessageConsumers",
                column: "Id");
        }
    }
}
