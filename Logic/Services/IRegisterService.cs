using Bolt.Models;
using Bolt.Models.ManageViewModels;
using System.Threading.Tasks;

namespace Bolt.Logic.Services
{
    public interface IRegisterService
    {
        Task<ApplicationUser> GetAndUpdateUser(IndexViewModel model);
    }
}
