using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cephiro.Listings.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "listing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AddresseStreet = table.Column<string>(name: "Addresse_Street", type: "text", nullable: false),
                    AddresseCountry = table.Column<string>(name: "Addresse_Country", type: "text", nullable: false),
                    AddresseCity = table.Column<string>(name: "Addresse_City", type: "text", nullable: false),
                    AddresseZipCode = table.Column<string>(name: "Addresse_ZipCode", type: "text", nullable: false),
                    AddresseLongitude = table.Column<double>(name: "Addresse_Longitude", type: "double precision", nullable: true),
                    AddresseLatitude = table.Column<double>(name: "Addresse_Latitude", type: "double precision", nullable: true),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    numberviews = table.Column<int>(name: "number_views", type: "integer", nullable: false),
                    priceday = table.Column<float>(name: "price_day", type: "real", nullable: false),
                    numberreserveddays = table.Column<int>(name: "number_reserved_days", type: "integer", nullable: false),
                    creationdate = table.Column<DateTime>(name: "creation_date", type: "timestamp with time zone", nullable: false),
                    averagestars = table.Column<float>(name: "average_stars", type: "real", nullable: false),
                    Name = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listing", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    ListingsId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_listing_ListingsId",
                        column: x => x.ListingsId,
                        principalTable: "listing",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tagstring = table.Column<string>(name: "tag_string", type: "text", nullable: false),
                    ListingsId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tag_listing_ListingsId",
                        column: x => x.ListingsId,
                        principalTable: "listing",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_ListingsId",
                table: "Image",
                column: "ListingsId");

            migrationBuilder.CreateIndex(
                name: "IX_tag_ListingsId",
                table: "tag",
                column: "ListingsId");

            migrationBuilder.CreateIndex(
                name: "IX_tag_tag_string",
                table: "tag",
                column: "tag_string",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "tag");

            migrationBuilder.DropTable(
                name: "listing");
        }
    }
}
