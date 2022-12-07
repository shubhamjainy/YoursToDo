using YoursToDo.Common.Entity;

namespace YoursToDo.Common.Interface
{
    public interface IUserService : IDataService<User>
    {
        Task<User> Get(string email);

        Task<bool> Exists(string email);
    }
}