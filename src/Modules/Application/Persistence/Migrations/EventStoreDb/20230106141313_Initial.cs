#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Ark.Application.Migrations.EventStoreDb;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            "EventStore");

        migrationBuilder.CreateTable(
            "ApplicationConfigurations",
            schema: "EventStore",
            columns: table => new
            {
                Id = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Description = table.Column<string>("character varying(1024)", maxLength: 1024, nullable: true),
                Value = table.Column<string>("character varying(512)", maxLength: 512, nullable: false),
                IsEncrypted = table.Column<bool>("boolean", nullable: false),
                IsDeleted = table.Column<bool>("boolean", nullable: false),
                DateCreated = table.Column<DateTimeOffset>("timestamp with time zone", nullable: false),
                DateModified = table.Column<DateTimeOffset>("timestamp with time zone", nullable: false),
                DateDeleted = table.Column<DateTimeOffset>("timestamp with time zone", nullable: true),
                CreatedBy = table.Column<string>("text", nullable: true),
                ModifiedBy = table.Column<string>("text", nullable: true),
                Version = table.Column<int>("integer", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ApplicationConfigurations", x => x.Id);
            });

        migrationBuilder.CreateTable(
            "AuditHistory",
            schema: "EventStore",
            columns: table => new
            {
                Id = table.Column<long>("bigint", nullable: false)
                          .Annotation("Npgsql:ValueGenerationStrategy",
                              NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                RowId = table.Column<string>("character varying(128)", maxLength: 128, nullable: false),
                TableName = table.Column<string>("character varying(128)", maxLength: 128, nullable: false),
                Changed = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: false),
                Kind = table.Column<int>("integer", nullable: false),
                Created = table.Column<DateTimeOffset>("timestamp with time zone", nullable: false),
                Username = table.Column<string>("character varying(128)", maxLength: 128, nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AuditHistory", x => x.Id);
            });

        migrationBuilder.CreateTable(
            "Events",
            schema: "EventStore",
            columns: table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                AggregateName = table.Column<string>("text", nullable: false),
                AggregateId = table.Column<string>("text", nullable: false),
                CreatedAt = table.Column<DateTimeOffset>("timestamp with time zone", nullable: false),
                Name = table.Column<string>("text", nullable: false),
                AssemblyTypeName = table.Column<string>("text", nullable: false),
                Data = table.Column<string>("text", nullable: false),
                Version = table.Column<int>("integer", nullable: false),
                IsDeleted = table.Column<bool>("boolean", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Events", x => x.Id);
            });

        migrationBuilder.CreateTable(
            "Snapshots",
            schema: "EventStore",
            columns: table => new
            {
                Id = table.Column<int>("integer", nullable: false)
                          .Annotation("Npgsql:ValueGenerationStrategy",
                              NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                AggregateId = table.Column<string>("text", nullable: false),
                AggregateName = table.Column<string>("text", nullable: false),
                LastAggregateVersion = table.Column<int>("integer", nullable: false),
                LastEventId = table.Column<Guid>("uuid", nullable: false),
                Data = table.Column<string>("text", nullable: false),
                IsDeleted = table.Column<bool>("boolean", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Snapshots", x => x.Id);
            });

        migrationBuilder.CreateTable(
            "BranchPoints",
            schema: "EventStore",
            columns: table => new
            {
                Id = table.Column<int>("integer", nullable: false)
                          .Annotation("Npgsql:ValueGenerationStrategy",
                              NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>("text", nullable: false),
                EventId = table.Column<Guid>("uuid", nullable: false),
                Type = table.Column<int>("integer", nullable: false),
                IsDeleted = table.Column<bool>("boolean", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BranchPoints", x => x.Id);
                table.ForeignKey(
                    "FK_BranchPoints_Events_EventId",
                    x => x.EventId,
                    principalSchema: "EventStore",
                    principalTable: "Events",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            "RetroactiveEvents",
            schema: "EventStore",
            columns: table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                BranchPointId = table.Column<int>("integer", nullable: false),
                Data = table.Column<string>("text", nullable: false),
                Sequence = table.Column<int>("integer", nullable: false),
                AssemblyTypeName = table.Column<string>("text", nullable: false),
                IsEnabled = table.Column<bool>("boolean", nullable: false),
                IsDeleted = table.Column<bool>("boolean", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RetroactiveEvents", x => x.Id);
                table.ForeignKey(
                    "FK_RetroactiveEvents_BranchPoints_BranchPointId",
                    x => x.BranchPointId,
                    principalSchema: "EventStore",
                    principalTable: "BranchPoints",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            "IX_BranchPoints_EventId",
            schema: "EventStore",
            table: "BranchPoints",
            column: "EventId");

        migrationBuilder.CreateIndex(
            "IX_BranchPoints_Name_EventId",
            schema: "EventStore",
            table: "BranchPoints",
            columns: new[] {"Name", "EventId"},
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Events_AggregateId_Version_AggregateName",
            schema: "EventStore",
            table: "Events",
            columns: new[] {"AggregateId", "Version", "AggregateName"},
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_RetroactiveEvents_BranchPointId",
            schema: "EventStore",
            table: "RetroactiveEvents",
            column: "BranchPointId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "ApplicationConfigurations",
            "EventStore");

        migrationBuilder.DropTable(
            "AuditHistory",
            "EventStore");

        migrationBuilder.DropTable(
            "RetroactiveEvents",
            "EventStore");

        migrationBuilder.DropTable(
            "Snapshots",
            "EventStore");

        migrationBuilder.DropTable(
            "BranchPoints",
            "EventStore");

        migrationBuilder.DropTable(
            "Events",
            "EventStore");
    }
}