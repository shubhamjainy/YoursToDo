﻿using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using YoursToDo.Common;
using YoursToDo.Common.Interface;
using YoursToDo.Common.Models;

namespace YoursToDo.Service
{
    internal sealed class UserService : DataService<User>, IUserService
    {
        UserDBContext context;

        public UserService() => context = App._context;
        public async Task<User> Get(string email) => (await context.Set<User>()
                .SingleOrDefaultAsync(user => user.Email == email))!;
    }
}