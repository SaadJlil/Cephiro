﻿// <auto-generated />
using System;
using Cephiro.Listings.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cephiro.Listings.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ListingsDbContext))]
    partial class ListingsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Cephiro.Listings.Domain.Entities.Listings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<float>("Average_stars")
                        .HasColumnType("real")
                        .HasColumnName("average_stars");

                    b.Property<DateTime>("Creation_date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("character varying(70)")
                        .HasColumnName("name");

                    b.Property<int>("Number_reserved_days")
                        .HasColumnType("integer")
                        .HasColumnName("number_reserved_days");

                    b.Property<float>("Price_day")
                        .HasColumnType("real")
                        .HasColumnName("price_day");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("listing_type");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("userid");

                    b.Property<int>("Views")
                        .HasColumnType("integer")
                        .HasColumnName("number_views");

                    b.HasKey("Id");

                    b.ToTable("listing");
                });

            modelBuilder.Entity("Cephiro.Listings.Domain.Entities.Photos", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<Guid>("ListingId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ListingId");

                    b.ToTable("image");
                });

            modelBuilder.Entity("Cephiro.Listings.Domain.Entities.Listings", b =>
                {
                    b.OwnsOne("Cephiro.Listings.Domain.ValueObjects.Location", "Addresse", b1 =>
                        {
                            b1.Property<Guid>("ListingsId")
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("city");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("country");

                            b1.Property<double?>("Latitude")
                                .HasColumnType("double precision")
                                .HasColumnName("latitude");

                            b1.Property<double?>("Longitude")
                                .HasColumnType("double precision")
                                .HasColumnName("longitude");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("street");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("zipcode");

                            b1.HasKey("ListingsId");

                            b1.ToTable("listing");

                            b1.WithOwner()
                                .HasForeignKey("ListingsId");
                        });

                    b.Navigation("Addresse")
                        .IsRequired();
                });

            modelBuilder.Entity("Cephiro.Listings.Domain.Entities.Photos", b =>
                {
                    b.HasOne("Cephiro.Listings.Domain.Entities.Listings", "Listing")
                        .WithMany("Images")
                        .HasForeignKey("ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Listing");
                });

            modelBuilder.Entity("Cephiro.Listings.Domain.Entities.Listings", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
