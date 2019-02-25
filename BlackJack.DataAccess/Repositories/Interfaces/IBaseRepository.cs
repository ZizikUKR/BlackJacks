using BlackJack.DataAccess.Entities;
using System;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task Add(T item);

        Task<T> Get(Guid id);

        Task Remove(T item);

        Task Update(T item);
    }
}
