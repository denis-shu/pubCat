using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bolt.Data;
using Bolt.Models;
using Bolt.Models.HomeViewModel;
using Bolt.Models.Menu;
using Microsoft.EntityFrameworkCore;

namespace Bolt.Logic.Services
{
    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext _db;

        public HomeService(ApplicationDbContext db)
        {
            _db = db;

        }

        public async Task<IndexViewModel> GetIndexViewModel()
        {
            return  new IndexViewModel
            {
                MenuItem = await GetMenuItems(),
                Category = await GetCategories(),
                Coupons = await GetCoupons()
            };
        }

        private async Task<List<MenuItem>> GetMenuItems()
        {
            return await _db.MenuItem
                .Include(c => c.Category)
                 .Include(s => s.SubCategory)
                 .ToListAsync();
        }

        private async Task<List<Category>> GetCategories()
        {
            return await _db.Category
                .OrderBy(s => s.DisplayOrder)
                .ToListAsync();
        }

        private async Task<List<Coupon>> GetCoupons()
        {
            return await _db.Coupons
                .Where(c => c.IsActive == true)
                .ToListAsync();
        }
    }
}
