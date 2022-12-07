using Microsoft.EntityFrameworkCore;
using YoursToDo.Common.Entity;
using YoursToDo.Common.Interface;

namespace YoursToDo.Common.Service
{
    public sealed class ItemService : DataService<Item>, IItemService
    {
        private readonly UserDBContext context;

        public ItemService(UserDBContext _context) : base(_context) => context = _context;

        public async Task<int> Insert(Item entity)
        {
            context.Entry(entity).State = EntityState.Added;
            return await context.SaveChangesAsync();
        }
    }
}