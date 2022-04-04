﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SPFxWorkshop.CouponAPI.Models;

namespace SPFxWorkshop.CouponAPI.Migrations
{
    [DbContext(typeof(CouponContext))]
    [Migration("20210610010024_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SPFxWorkshop.CouponAPI.Models.Coupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CouponCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("Owner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RedeemedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Coupons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CouponCode = "COUPON000",
                            Expiration = new DateTime(2031, 6, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Owner = "frank@m365x725618.onmicrosoft.com",
                            RedeemedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CouponCode = "COUPON001",
                            Expiration = new DateTime(2031, 6, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Owner = "frank@m365x725618.onmicrosoft.com",
                            RedeemedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            CouponCode = "COUPON002",
                            Expiration = new DateTime(2031, 6, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Owner = "frank@m365x725618.onmicrosoft.com",
                            RedeemedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            CouponCode = "COUPON003",
                            Expiration = new DateTime(2031, 6, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Owner = "frank@m365x725618.onmicrosoft.com",
                            RedeemedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            CouponCode = "COUPON004",
                            Expiration = new DateTime(2031, 6, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Owner = "frank@m365x725618.onmicrosoft.com",
                            RedeemedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 6,
                            CouponCode = "COUPON005",
                            Expiration = new DateTime(2031, 6, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Owner = "frank@m365x725618.onmicrosoft.com",
                            RedeemedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 7,
                            CouponCode = "COUPON006",
                            Expiration = new DateTime(2031, 6, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Owner = "frank@m365x725618.onmicrosoft.com",
                            RedeemedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 8,
                            CouponCode = "COUPON007",
                            Expiration = new DateTime(2031, 6, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Owner = "frank@m365x725618.onmicrosoft.com",
                            RedeemedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 9,
                            CouponCode = "COUPON008",
                            Expiration = new DateTime(2031, 6, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Owner = "frank@m365x725618.onmicrosoft.com",
                            RedeemedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 10,
                            CouponCode = "COUPON009",
                            Expiration = new DateTime(2031, 6, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Owner = "frank@m365x725618.onmicrosoft.com",
                            RedeemedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}