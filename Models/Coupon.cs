using System;
using System.ComponentModel.DataAnnotations;

namespace Bolt.Models
{
    public class Coupon
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CouponTYpe { get; set; }

        [Required]
        public double Discount { get; set; }

        [Required]
        public double MinimumAmout  { get; set; }

        [Required]
        public byte[] Picture { get; set; }

        public bool IsActive { get; set; }
    }
}
