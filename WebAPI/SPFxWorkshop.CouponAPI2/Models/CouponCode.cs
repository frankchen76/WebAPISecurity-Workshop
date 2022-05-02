using System;
using System.Collections.Generic;

namespace SPFxWorkshop.CouponAPI2.Models
{
    public partial class CouponCode
    {
        public int Id { get; set; }
        public string? EncryptedCouponCode { get; set; }
        public string? Owner { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime RedeemedDate { get; set; }
    }
}
