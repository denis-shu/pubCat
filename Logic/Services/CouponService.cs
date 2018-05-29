using Bolt.Data;
using Bolt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Bolt.Logic.Services
{
    public class CouponService : ICouponService
    {
        private readonly ApplicationDbContext _db;

        public CouponService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Coupon>> GetCouponsAsync()
        {
            return await _db.Coupons.ToListAsync();
        }

        public async Task<bool> CreateCouponAsync(IFormFileCollection files, Coupon coupon)
        {
            if (files[0] != null && files[0].Length > 0)
            {
                byte[] pi = null;
                using (var fs = files[0].OpenReadStream())
                {
                    using (var ms = new MemoryStream())
                    {
                        await fs.CopyToAsync(ms);
                        pi = ms.ToArray();
                    }
                }
                coupon.Picture = pi;
                _db.Coupons.Add(coupon);
                

            }
            return (await _db.SaveChangesAsync() > 0);
        }
    }
}
