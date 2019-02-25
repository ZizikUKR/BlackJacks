using BlackJack.DataAccess.Entities;
using BlackJack.DataAccess.Repositories.Interfaces;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.EntityFrameworkRepositories
{
    public class EFBaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected BlackJackContext _dbContext;
        public EFBaseRepository(BlackJackContext context)
        {
            _dbContext = context;
        }
        public virtual async Task Add(T item)
        {
            _dbContext.Set<T>().Add(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> Get(Guid id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(m => m.Id == id);

        }

        public async Task Remove(T item)
        {
            _dbContext.Set<T>().Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T item)
        {
            _dbContext.Entry<T>(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
