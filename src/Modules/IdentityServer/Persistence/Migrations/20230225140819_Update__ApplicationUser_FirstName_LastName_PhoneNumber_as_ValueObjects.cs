using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ark.IdentityServer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Update__ApplicationUser_FirstName_LastName_PhoneNumber_as_ValueObjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "IdentityServer",
                table: "ApplicationUsers",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "IdentityServer",
                table: "ApplicationUsers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "IdentityServer",
                table: "ApplicationUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "IdentityServer",
                table: "ApplicationUsers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "IdentityServer",
                table: "ApplicationUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.RenameColumn(
                name: "Password",
                schema: "IdentityServer",
                table: "ApplicationUsers",
                newName: "HashedPassword");
            
            migrationBuilder.AlterColumn<string>(
                name: "HashedPassword",
                schema: "IdentityServer",
                table: "ApplicationUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashedPassword",
                schema: "IdentityServer",
                table: "ApplicationUsers");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "IdentityServer",
                table: "ApplicationUsers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "IdentityServer",
                table: "ApplicationUsers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "IdentityServer",
                table: "ApplicationUsers",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "IdentityServer",
                table: "ApplicationUsers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "IdentityServer",
                table: "ApplicationUsers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.RenameColumn(
                name: "HashedPassword",
                schema: "IdentityServer",
                table: "ApplicationUsers",
                newName: "Password");
            
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                schema: "IdentityServer",
                table: "ApplicationUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
