using Bolt.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bolt.Logic.Services
{
    public interface ICouponService
    {
        Task<List<Coupon>> GetCouponsAsync();
        Task<Coupon> GetCouponAsync(Guid? id);
        Task<bool> CreateCouponAsync(IFormFileCollection files, Coupon coupon);
        Task<bool> Delete(Guid id);
    }
}
