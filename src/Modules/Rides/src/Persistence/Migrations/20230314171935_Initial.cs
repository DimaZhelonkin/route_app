using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ark.Rides.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Rides");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "AuditHistory",
                schema: "Rides",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RowId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    TableName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Changed = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    Kind = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Username = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DriverRides",
                schema: "Rides",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DeletedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    RouteId = table.Column<Guid>(type: "uuid", nullable: false),
                    FactRoute = table.Column<LineString>(type: "Geometry (LineString)", nullable: true),
                    StartedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    FinishedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CanceledAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverRides", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                schema: "Rides",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsLicensed = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DeletedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    IdentityId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                schema: "Rides",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    OccurredOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ProcessedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    PublishedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Error = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                schema: "Rides",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DeletedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    IdentityId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PassengerRides",
                schema: "Rides",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PassengerId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DeletedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    RouteId = table.Column<Guid>(type: "uuid", nullable: false),
                    FactRoute = table.Column<LineString>(type: "Geometry (LineString)", nullable: true),
                    StartedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    FinishedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CanceledAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerRides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassengerRides_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalSchema: "Rides",
                        principalTable: "Passengers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverRidePassengerRide",
                schema: "Rides",
                columns: table => new
                {
                    DriversRidesId = table.Column<Guid>(type: "uuid", nullable: false),
                    PassengersRidesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverRidePassengerRide", x => new { x.DriversRidesId, x.PassengersRidesId });
                    table.ForeignKey(
                        name: "FK_DriverRidePassengerRide_DriverRides_DriversRidesId",
                        column: x => x.DriversRidesId,
                        principalSchema: "Rides",
                        principalTable: "DriverRides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverRidePassengerRide_PassengerRides_PassengersRidesId",
                        column: x => x.PassengersRidesId,
                        principalSchema: "Rides",
                        principalTable: "PassengerRides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RideRequests",
                schema: "Rides",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DriverRideId = table.Column<Guid>(type: "uuid", nullable: false),
                    PassengerRideId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DeletedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RideRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RideRequests_DriverRides_DriverRideId",
                        column: x => x.DriverRideId,
                        principalSchema: "Rides",
                        principalTable: "DriverRides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RideRequests_PassengerRides_PassengerRideId",
                        column: x => x.PassengerRideId,
                        principalSchema: "Rides",
                        principalTable: "PassengerRides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverRidePassengerRide_PassengersRidesId",
                schema: "Rides",
                table: "DriverRidePassengerRide",
                column: "PassengersRidesId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverRides_RouteId",
                schema: "Rides",
                table: "DriverRides",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverRides_VehicleId",
                schema: "Rides",
                table: "DriverRides",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_IdentityId",
                schema: "Rides",
                table: "Drivers",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerRides_PassengerId",
                schema: "Rides",
                table: "PassengerRides",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerRides_RouteId",
                schema: "Rides",
                table: "PassengerRides",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_IdentityId",
                schema: "Rides",
                table: "Passengers",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_RideRequests_DriverRideId",
                schema: "Rides",
                table: "RideRequests",
                column: "DriverRideId");

            migrationBuilder.CreateIndex(
                name: "IX_RideRequests_PassengerRideId",
                schema: "Rides",
                table: "RideRequests",
                column: "PassengerRideId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditHistory",
                schema: "Rides");

            migrationBuilder.DropTable(
                name: "DriverRidePassengerRide",
                schema: "Rides");

            migrationBuilder.DropTable(
                name: "Drivers",
                schema: "Rides");

            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "Rides");

            migrationBuilder.DropTable(
                name: "RideRequests",
                schema: "Rides");

            migrationBuilder.DropTable(
                name: "DriverRides",
                schema: "Rides");

            migrationBuilder.DropTable(
                name: "PassengerRides",
                schema: "Rides");

            migrationBuilder.DropTable(
                name: "Passengers",
                schema: "Rides");
        }
    }
}
