using System.Threading.Tasks;
using Bolt.Logic.Services;
using Bolt.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bolt.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _service;

        public CouponController(ICouponService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetCouponsAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Coupon c)
        {
            var files = HttpContext.Request.Form.Files;

            var res = await _service.CreateCouponAsync(files, c);

            if (res)
                RedirectToAction(nameof(Index));

            return View(c);
        }
    }
}