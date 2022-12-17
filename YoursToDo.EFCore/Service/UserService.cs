using Microsoft.EntityFrameworkCore;
using YoursToDo.EFCore.Entity;
using YoursToDo.EFCore.Interface;

namespace YoursToDo.EFCore.Service
{
    public sealed class UserService : DataService<User>, IUserService
    {
        private readonly UserDBContext context;

        public UserService(UserDBContext _context) : base(_context) => context = _context;

        public async Task<User> Get(string email) => (await context.Set<User>()
                .SingleOrDefaultAsync(user => user.Email == email))!;

        public async Task<bool> Exists(string email) => (await context.Set<User>()
               .AnyAsync(user => user.Email == email))!;
    }
}