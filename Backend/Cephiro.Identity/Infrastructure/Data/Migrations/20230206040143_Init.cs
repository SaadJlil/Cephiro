using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cephiro.Identity.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "identity");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "metrics",
                schema: "identity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    lockoutenabled = table.Column<bool>(name: "lockout_enabled", type: "boolean", nullable: false),
                    usercount = table.Column<int>(name: "user_count", type: "integer", nullable: false),
                    activeusercount = table.Column<int>(name: "active_user_count", type: "integer", nullable: false),
                    unverifiedusercount = table.Column<int>(name: "unverified_user_count", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_metrics", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "identity",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    imageuri = table.Column<string>(name: "image_uri", type: "character varying(400)", maxLength: 400, nullable: true),
                    description = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true),
                    firstname = table.Column<string>(name: "first_name", type: "character varying(256)", maxLength: 256, nullable: false),
                    middlename = table.Column<string>(name: "middle_name", type: "character varying(128)", maxLength: 128, nullable: true),
                    lastname = table.Column<string>(name: "last_name", type: "character varying(128)", maxLength: 128, nullable: false),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    emailconfirmed = table.Column<bool>(name: "email_confirmed", type: "boolean", nullable: false),
                    phonenumber = table.Column<string>(name: "phone_number", type: "character varying(16)", maxLength: 16, nullable: true),
                    phonenumberconfirmed = table.Column<bool>(name: "phone_number_confirmed", type: "boolean", nullable: false),
                    passwordhash = table.Column<byte[]>(name: "password_hash", type: "bytea", nullable: true),
                    passwordsalt = table.Column<byte[]>(name: "password_salt", type: "bytea", nullable: true),
                    hascreditcard = table.Column<bool>(name: "has_credit_card", type: "boolean", nullable: false),
                    regularizationstage = table.Column<int>(name: "regularization_stage", type: "integer", nullable: false),
                    status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.UniqueConstraint("AK_user_email", x => x.email);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_email",
                schema: "identity",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_phone_number",
                schema: "identity",
                table: "user",
                column: "phone_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "metrics",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "user",
                schema: "identity");
        }
    }
}
