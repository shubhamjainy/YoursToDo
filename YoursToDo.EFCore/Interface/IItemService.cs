using YoursToDo.EFCore.Entity;

namespace YoursToDo.EFCore.Interface
{
    public interface IItemService : IDataService<Item>
    {
        Task<int> Insert(Item entity);
    }
}