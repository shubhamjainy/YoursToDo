using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using YoursToDo.Common;
using YoursToDo.Common.Interface;
using YoursToDo.Common.Models;

namespace YoursToDo.WinUI.Service
{
    internal sealed class ItemService : DataService<Item>, IItemService
    {
        UserDBContext context;

        public ItemService() => context = App._context;

        public async Task<int> Insert(Item entity)
        {
            context.Entry(entity).State = EntityState.Added;
            return await context.SaveChangesAsync();
        }
    }
}
