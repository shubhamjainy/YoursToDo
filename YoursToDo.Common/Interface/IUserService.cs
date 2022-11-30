using System.Threading.Tasks;
using YoursToDo.Common.Models;

namespace YoursToDo.Common.Interface
{
    public interface IUserService : IDataService<User>
    {
        Task<User> Get(string email);
    }
}