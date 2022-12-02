using YoursToDo.Common.Models;

namespace YoursToDo.Common.Interface
{
    public interface IItemService : IDataService<Item>
    {
        Task<int> Insert(Item entity);
    }
}