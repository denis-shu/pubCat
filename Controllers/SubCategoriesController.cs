using System;
using System.Linq;
using System.Threading.Tasks;
using Bolt.Data;
using Bolt.Logic.Services;
using Bolt.Models;
using Bolt.Models.SubCategotyViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bolt.Controllers
{
    public class SubCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SubCategoryServices _subService;

        [TempData]
        public string StatusMessage { get; set; }

        public SubCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var subs = _context.SubCategory.Include(i => i.Category);

            return View(await subs.ToListAsync());
        }

        //Get
        public IActionResult Create()
        {
            var model = new SubCCViewModel
            {
                CategoryList = _context.Category.ToList(),
                SubCategory = new SubCategory(),
                SubCategoryList = _context.SubCategory
                .OrderBy(p => p.Name)
                .Select(p => p.Name)
                .ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubCCViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isSubCategoryExist = _context.SubCategory.Include(c => c.Category).Any(a => a.Name == model.SubCategory.Name);

                var isSubCategorAndCatyExist = _context.SubCategory.Any(a => a.Name == model.SubCategory.Name
                && a.CategoryId == model.SubCategory.CategoryId);

                if (isSubCategoryExist && model.IsNew)
                {
                    StatusMessage = "Error: Sub Category Name already exist";
                }
                else
                {
                    if (!isSubCategoryExist && !model.IsNew)
                    {
                        StatusMessage = "Sub Category Name does not exist";
                    }
                    else
                    {
                        if (isSubCategorAndCatyExist)
                        {
                            StatusMessage = "Error: Cat and SubCat combination exist";
                        }
                        else
                        {
                            _context.Category.FirstOrDefault(x => x.Id == model.SubCategory.CategoryId);
                            _context.Add(model.SubCategory);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }
            var modelVM = new SubCCViewModel
            {
                CategoryList = _context.Category.ToList(),
                SubCategory = model.SubCategory,
                SubCategoryList = _context.SubCategory
               .OrderBy(p => p.Name)
               .Select(p => p.Name)
               .ToList(),
                StatusMessage = StatusMessage
            };
            return View(modelVM);

        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var subCategory = await _context.SubCategory
                .SingleOrDefaultAsync(x => x.Id == id);

            if (subCategory == null)
                return NotFound();

            var model = new SubCCViewModel
            {
                CategoryList = _context.Category.ToList(),
                SubCategory = subCategory,
                SubCategoryList = _context.SubCategory
                .Select(s => s.Name).Distinct().ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SubCCViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isSubCategoryExist = _context.SubCategory.Include(c => c.Category).Any(a => a.Name == model.SubCategory.Name);

                var isSubCategorAndCatyExist = _context.SubCategory.Any(a => a.Name == model.SubCategory.Name
                && a.CategoryId == model.SubCategory.CategoryId);

                if (!isSubCategoryExist)
                {
                    StatusMessage = "Error. Sub Categ doesnt exist. you cannot add a new subcategory here";
                }
                else
                {
                    if (isSubCategorAndCatyExist)
                    {
                        StatusMessage = "Error. Categoty adn sub comination already exist";
                    }
                    else
                    {
                        var subCatFromBase = _context.SubCategory.Find(id);
                        subCatFromBase.Name = model.SubCategory.Name;
                        subCatFromBase.CategoryId = model.SubCategory.CategoryId;

                        await _context.SaveChangesAsync();

                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            var modelVM = new SubCCViewModel
            {
                CategoryList = _context.Category.ToList(),
                SubCategory = model.SubCategory,
                SubCategoryList = _context.SubCategory
                   .Select(s => s.Name).Distinct().ToList(),
                StatusMessage = StatusMessage
            };
            return View(modelVM);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
                return NotFound();

            var sub = await _context.SubCategory
                .Include(x => x.Category)
                .SingleOrDefaultAsync(d => d.Id == id);

            if (sub == null)
                return NotFound();

            return View(sub);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
                return NotFound();

            var sub = await _context.SubCategory
                .Include(x => x.Category)
                .SingleOrDefaultAsync(d => d.Id == id);

            if (sub == null)
                return NotFound();

            return View(sub);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (id == null)
                return NotFound();

            var sub = await _context.SubCategory   
                .SingleOrDefaultAsync(d => d.Id == id);

            if (sub == null)
                return NotFound();

            _context.Remove(sub);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}