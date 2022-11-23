using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SPFxWorkshop.CouponAPI3.Models
{
    public partial class CouponContext : DbContext
    {
        public CouponContext()
        {
        }

        public CouponContext(DbContextOptions<CouponContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Coupon> Coupons { get; set; } = null!;
        public virtual DbSet<CouponCode> CouponCodes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:CouponDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CouponCode>(entity =>
            {
                entity.ToTable("CouponCode");

                entity.Property(e => e.EncryptedCouponCode).UseCollation("Latin1_General_BIN2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
