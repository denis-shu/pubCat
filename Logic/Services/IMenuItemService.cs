using Bolt.Models;
using Bolt.Models.Menu;
using Bolt.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bolt.Logic.Services
{
    public interface IMenuItemService
    {
        IIncludableQueryable<MenuItem, SubCategory> GetMenuItems();
        MenuItemViewModel GetMenuItemVM();
        List<Category> GetCategory();
        List<SubCategory> GetSubCat(Guid id);
        Task SaveAsync(MenuItem item);
        Task<MenuItem> GetMenuItemByIdAsync(Guid id);
        Task<bool> AddSaveFoto(IFormFileCollection files, string root, MenuItemViewModel model);
        Task<MenuItem> GetFullMenuItem(Guid id);
        Task<IEnumerable<SubCategory>> GetSubcategory(Guid id);
        Task<bool> EditAsync(IFormFileCollection files, string root, MenuItemViewModel model);


    }
}
