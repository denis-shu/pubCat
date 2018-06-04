using Bolt.Models.Menu;
using System.Collections.Generic;

namespace Bolt.Models.HomeViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<MenuItem> MenuItem { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<Coupon> Coupons { get; set; }

        public string StatusMessage { get; set; }
    }
}
