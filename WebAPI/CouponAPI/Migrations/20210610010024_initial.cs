using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPFxWorkshop.CouponAPI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CouponCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RedeemedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "Id", "CouponCode", "Expiration", "Owner", "RedeemedDate" },
                values: new object[,]
                {
                    { 1, "COUPON000", new DateTime(2031, 6, 9, 0, 0, 0, 0, DateTimeKind.Local), "frank@m365x725618.onmicrosoft.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "COUPON001", new DateTime(2031, 6, 9, 0, 0, 0, 0, DateTimeKind.Local), "frank@m365x725618.onmicrosoft.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "COUPON002", new DateTime(2031, 6, 9, 0, 0, 0, 0, DateTimeKind.Local), "frank@m365x725618.onmicrosoft.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "COUPON003", new DateTime(2031, 6, 9, 0, 0, 0, 0, DateTimeKind.Local), "frank@m365x725618.onmicrosoft.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "COUPON004", new DateTime(2031, 6, 9, 0, 0, 0, 0, DateTimeKind.Local), "frank@m365x725618.onmicrosoft.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "COUPON005", new DateTime(2031, 6, 9, 0, 0, 0, 0, DateTimeKind.Local), "frank@m365x725618.onmicrosoft.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "COUPON006", new DateTime(2031, 6, 9, 0, 0, 0, 0, DateTimeKind.Local), "frank@m365x725618.onmicrosoft.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "COUPON007", new DateTime(2031, 6, 9, 0, 0, 0, 0, DateTimeKind.Local), "frank@m365x725618.onmicrosoft.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "COUPON008", new DateTime(2031, 6, 9, 0, 0, 0, 0, DateTimeKind.Local), "frank@m365x725618.onmicrosoft.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "COUPON009", new DateTime(2031, 6, 9, 0, 0, 0, 0, DateTimeKind.Local), "frank@m365x725618.onmicrosoft.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons");
        }
    }
}
