using Microsoft.EntityFrameworkCore;
using YoursToDo.EFCore.Entity;
using YoursToDo.EFCore.Interface;

namespace YoursToDo.EFCore.Service
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