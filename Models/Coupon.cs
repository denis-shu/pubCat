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
        public enum ECouponType
        {
            Persent = 0,
            Dollar = 1
        }


        [Required]
        public double MinimumAmout  { get; set; }

        public byte[] Picture { get; set; }

        public bool IsActive { get; set; }
    }
}
