﻿using System;
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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    street = table.Column<string>(type: "text", nullable: false),
                    country = table.Column<string>(type: "text", nullable: false),
                    city = table.Column<string>(type: "text", nullable: false),
                    zipcode = table.Column<string>(type: "text", nullable: false),
                    longitude = table.Column<double>(type: "double precision", nullable: true),
                    latitude = table.Column<double>(type: "double precision", nullable: true),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    priceday = table.Column<float>(name: "price_day", type: "real", nullable: false),
                    numberreserveddays = table.Column<int>(name: "number_reserved_days", type: "integer", nullable: false),
                    creationdate = table.Column<DateTime>(name: "creation_date", type: "timestamp with time zone", nullable: false),
                    listingtype = table.Column<int>(name: "listing_type", type: "integer", nullable: false),
                    averagestars = table.Column<float>(name: "average_stars", type: "real", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    numberviews = table.Column<int>(name: "number_views", type: "integer", nullable: false),
                    numberreviews = table.Column<int>(name: "number_reviews", type: "integer", nullable: false),
                    beds = table.Column<int>(type: "integer", nullable: false),
                    bedrooms = table.Column<int>(type: "integer", nullable: false),
                    bathrooms = table.Column<int>(type: "integer", nullable: false),
                    wifi = table.Column<bool>(type: "boolean", nullable: false),
                    airconditioning = table.Column<bool>(type: "boolean", nullable: false),
                    smoking = table.Column<bool>(type: "boolean", nullable: false),
                    washingmachine = table.Column<bool>(name: "washing_machine", type: "boolean", nullable: false),
                    dishwasher = table.Column<bool>(name: "dish_washer", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listing", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reservation",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    listingid = table.Column<Guid>(type: "uuid", nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    reservationdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    startdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    enddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    reviewstar = table.Column<int>(type: "integer", nullable: true),
                    reviewdescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservation", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "image",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ListingId = table.Column<Guid>(type: "uuid", nullable: false),
                    imageuri = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image", x => x.id);
                    table.ForeignKey(
                        name: "FK_image_listing_ListingId",
                        column: x => x.ListingId,
                        principalTable: "listing",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_image_ListingId",
                table: "image",
                column: "ListingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "image");

            migrationBuilder.DropTable(
                name: "reservation");

            migrationBuilder.DropTable(
                name: "listing");
        }
    }
}
