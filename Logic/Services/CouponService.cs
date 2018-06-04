using Bolt.Data;
using Bolt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<Coupon> GetCouponAsync(Guid? id)
        {
            return await _db.Coupons
                 .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> CreateCouponAsync(IFormFileCollection files, Coupon coupon)
        {
            if (coupon.Id == Guid.Empty)
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
            }
            else
            {
                var cDb = await GetCouponAsync(coupon.Id);

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

                }
                cDb.MinimumAmout = coupon.MinimumAmout;
                cDb.Name = coupon.Name;
                cDb.Discount = coupon.Discount;
                cDb.CouponTYpe = coupon.CouponTYpe;
                cDb.IsActive = coupon.IsActive;

                _db.Entry(cDb).State = EntityState.Modified;
                // _db.Coupons.Add(cDb);

            }
            return (await _db.SaveChangesAsync() > 0);
        }

        public async Task<bool> Delete(Guid id)
        {
            var c = await GetCouponAsync(id);

            if (c != null)
            {
                _db.Coupons.Remove(c);
               await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
