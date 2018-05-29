using Bolt.Data;
using Bolt.Models;
using Bolt.Models.Menu;
using Bolt.Models.ViewModels;
using Bolt.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bolt.Logic.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public MenuItemViewModel MenuItemVm { get; set; }

        public MenuItemService(ApplicationDbContext db)
        {
            _db = db;
            MenuItemVm = new MenuItemViewModel
            {
                Category = _db.Category.ToList(),
                MenuItem = new MenuItem()
            };
        }

        public IIncludableQueryable<MenuItem, SubCategory> GetMenuItems()
        {
            return _db.MenuItem.Include(c => c.Category)
                 .Include(s => s.SubCategory);
        }

        public MenuItemViewModel GetMenuItemVM() => MenuItemVm;


        public List<Category> GetCategory() => _db.Category.ToList();


        public List<SubCategory> GetSubCat(Guid id)
        {
            return _db.SubCategory.Where(s => s.CategoryId == id)
                .ToList();
        }

        public async Task SaveAsync(MenuItem item)
        {
            _db.MenuItem.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task<MenuItem> GetMenuItemByIdAsync(Guid id)
        {
            return await _db.MenuItem.FindAsync(id);
        }

        public async Task<bool> AddSaveFoto(IFormFileCollection files, string root, MenuItemViewModel model)
        {
            var mI = await GetMenuItemByIdAsync(model.MenuItem.Id);

            if (files[0] != null && files[0].Length > 0)
            {
                var uploads = Path.Combine(root, "images");

                var extension = files[0].FileName.Substring(files[0].FileName.LastIndexOf("."),
                    files[0].FileName.Length - files[0].FileName.LastIndexOf("."));

                using (var fs = new FileStream(Path.Combine(uploads, model.MenuItem.Id + extension),
                    FileMode.Create))
                {
                    files[0].CopyTo(fs);
                }

                mI.Image = @"\images\" + model.MenuItem.Id + extension;
            }
            else
            {
                var uploads = Path.Combine(root, @"images\" + SD.DefaultFoodImage);

                System.IO.File.Copy(uploads, root + @"\images\" + model.MenuItem.Id + ".jpg");

                mI.Image = @"\images\" + model.MenuItem.Id + ".jpg";
            }
            return await _db.SaveChangesAsync() > 0;

        }

        public async Task<MenuItem> GetFullMenuItem(Guid id)
        {
            return await _db.MenuItem.Include(s => s.Category)
                .Include(s => s.SubCategory)
                .SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<SubCategory>> GetSubcategory(Guid id)
        {
            return await _db.SubCategory
                .Where(s => s.CategoryId == id)
                .ToListAsync();
        }

        public async Task<bool> EditAsync(IFormFileCollection files, string root, MenuItemViewModel model)
        {
            var mI = await GetMenuItemByIdAsync(model.MenuItem.Id);

            if (files[0] != null && files[0].Length > 0)
            {
                var uploads = Path.Combine(root, "images");

                var extension_new = files[0].FileName.Substring(files[0].FileName.LastIndexOf("."),
                    files[0].FileName.Length - files[0].FileName.LastIndexOf("."));

                var extension_old = mI.Image.Substring(mI.Image.LastIndexOf("."),
                    mI.Image.Length - mI.Image.LastIndexOf("."));

                if (System.IO.File.Exists(Path.Combine(uploads, model.MenuItem.Id + extension_old)))
                {
                    System.IO.File.Delete(Path.Combine(uploads, model.MenuItem.Id + extension_old));
                }

                using (var fs = new FileStream(Path.Combine(uploads, model.MenuItem.Id + extension_new),
                    FileMode.Create))
                {
                    files[0].CopyTo(fs);
                }

                model.MenuItem.Image = @"\images\" + model.MenuItem.Id + extension_new;
            }
            if (model.MenuItem.Image != null)
                mI.Image = model.MenuItem.Image;

            mI.Name = model.MenuItem.Name;
            mI.Price = model.MenuItem.Price;
            mI.Description = model.MenuItem.Description;
            mI.Spicy = model.MenuItem.Spicy;
            mI.CategoryId = model.MenuItem.CategoryId;
            mI.SubCategoryId = model.MenuItem.SubCategoryId;
           
            return await _db.SaveChangesAsync() > 0;

        }
    }
}

