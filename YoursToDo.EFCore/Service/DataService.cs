using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using YoursToDo.EFCore.Interface;

namespace YoursToDo.EFCore.Service
{
    public abstract class DataService<T> : IDataService<T> where T : class
    {
        private readonly UserDBContext context;

        public DataService(UserDBContext _context) => context = _context;

        public async Task<T> Get(int id) => (await context.Set<T>().FindAsync(id))!;

        public async Task<IEnumerable<T>> GetAll() => await context.Set<T>().ToListAsync();

        public async Task<T> Create(T entity)
        {
            EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return createdResult.Entity;
        }

        public async Task<bool> Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<T> Update(T entity)
        {
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}