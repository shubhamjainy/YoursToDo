using YoursToDo.EFCore.Entity;

namespace YoursToDo.EFCore.Interface
{
    public interface IUserService : IDataService<User>
    {
        Task<User> Get(string email);

        Task<bool> Exists(string email);
    }
}