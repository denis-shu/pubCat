using System;
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
                return RedirectToAction(nameof(Index));

            return View(c);
        }


        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var c = await _service.GetCouponAsync(id);

            if (c == null)
                return NotFound();

            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Coupon c)
        {
            if (id != c.Id)
                return NotFound();

            var res = await _service.CreateCouponAsync(HttpContext.Request.Form.Files, c);

            if (res)
                return RedirectToAction(nameof(Index));

            return View(c);           
        }

        
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var c = await _service.GetCouponAsync(id);

            if (c == null)
                return NotFound();

            return View(c);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var res = await _service.Delete(id);

            if (res)
                return RedirectToAction(nameof(Index));

            return BadRequest();
        }
    }
}