using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using YoursToDo.Common;
using YoursToDo.Common.Interface;
using YoursToDo.Common.Models;

namespace YoursToDo.WinUI.Service
{
    internal sealed class UserService : DataService<User>, IUserService
    {
        UserDBContext context;

        public UserService() => context = App._context;
        public async Task<User> Get(string email) => (await context.Set<User>()
                .SingleOrDefaultAsync(user => user.Email == email))!;

        public async Task<bool> Exists(string email) => (await context.Set<User>()
              .AnyAsync(user => user.Email == email))!;
    }
}