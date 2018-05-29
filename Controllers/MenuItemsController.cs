using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bolt.Logic.Services;
using Bolt.Models.Menu;
using Bolt.Models.ViewModels;
using Bolt.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bolt.Controllers
{
    public class MenuItemsController : Controller
    {
        private readonly IMenuItemService _service;
        private readonly IHostingEnvironment _hostEnv;
        [BindProperty]
        public MenuItemViewModel MenuItemVm { get; set; }



        public MenuItemsController(IMenuItemService service, IHostingEnvironment hostEnv)
        {
            _service = service;
            _hostEnv = hostEnv;
            MenuItemVm = new MenuItemViewModel
            {
                Category = _service.GetCategory(),
                MenuItem = new Models.Menu.MenuItem()
            };

        }
        public async Task<IActionResult> Index()
        {
            var menuItems = _service.GetMenuItems();

            return View(await menuItems.ToListAsync());
        }

        public IActionResult Create()
        {
            return View(MenuItemVm);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            MenuItemVm.MenuItem.SubCategoryId = new Guid(Request.Form["SubCategoryId"].ToString());

            if (!ModelState.IsValid)
                return View();

            await _service.SaveAsync(MenuItemVm.MenuItem);

            string root = _hostEnv.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var res = _service.AddSaveFoto(files, root, MenuItemVm);
            //var mI = await _service.GetMenuItemByIdAsync(MenuItemVm.MenuItem.Id);

            //if (files[0] != null && files[0].Length > 0)
            //{
            //    var uploads = Path.Combine(root, "images");

            //    var extension = files[0].FileName.Substring(files[0].FileName.LastIndexOf("."),
            //        files[0].FileName.Length - files[0].FileName.LastIndexOf("."));

            //    using (var fs = new FileStream(Path.Combine(uploads, MenuItemVm.MenuItem.Id + extension),
            //        FileMode.Create))
            //    {
            //        files[0].CopyTo(fs);
            //    }

            //    mI.Image = @"\images\" + MenuItemVm.MenuItem.Id + extension;
            //}
            //else
            //{
            //    var uploads = Path.Combine(root, @"images\" + SD.DefaultFoodImage);

            //    System.IO.File.Copy(uploads, root + @"\images\" + MenuItemVm.MenuItem.Id + ".jpg");

            //    mI.Image= @"\images\" + MenuItemVm.MenuItem.Id + ".jpg";
            //}

            return RedirectToAction(nameof(Index));


        }
        public JsonResult GetSubCategory(Guid CategoryId)
        {
            return Json(new SelectList(_service.GetSubCat(CategoryId), "Id", "Name"));
        }

        [HttpGet, ActionName("Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
                return NotFound();

            MenuItemVm.MenuItem = await _service.GetFullMenuItem(id);
            MenuItemVm.SubCategory = await _service.GetSubcategory(MenuItemVm.MenuItem.CategoryId);

            if (MenuItemVm.MenuItem == null)
                return NotFound();

            return View(MenuItemVm);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(Guid id)
        {
            MenuItemVm.MenuItem.SubCategoryId = new Guid(Request.Form["SubCategoryId"].ToString());

            if (id != MenuItemVm.MenuItem.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    string root = _hostEnv.WebRootPath;
                    var files = HttpContext.Request.Form.Files;
                    var res = await _service.EditAsync(files, root, MenuItemVm);
                }
                catch (Exception)
                {

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                MenuItemVm.SubCategory = await _service
                    .GetSubcategory(MenuItemVm.MenuItem.SubCategoryId);
                return View(MenuItemVm);
            }
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
                return NotFound();

            MenuItemVm.MenuItem = await _service.GetFullMenuItem(id);

            if (MenuItemVm.MenuItem == null)
                return NotFound();

            return View(MenuItemVm);
        }
    }
}