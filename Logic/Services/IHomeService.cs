using Bolt.Models.HomeViewModel;
using System.Threading.Tasks;

namespace Bolt.Logic.Services
{
    public interface IHomeService
    {
        Task<IndexViewModel> GetIndexViewModel();
    }
}
