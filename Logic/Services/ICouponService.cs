using Bolt.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bolt.Logic.Services
{
    public interface ICouponService
    {
        Task<List<Coupon>> GetCouponsAsync();
        Task<bool> CreateCouponAsync(IFormFileCollection files, Coupon coupon);
    }
}
