using System;
using System.Collections.Generic;

namespace SPFxWorkshop.CouponAPI3.Models
{
    public partial class Coupon
    {
        public int Id { get; set; }
        public string? CouponCode { get; set; }
        public string? Owner { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime RedeemedDate { get; set; }
    }
}
