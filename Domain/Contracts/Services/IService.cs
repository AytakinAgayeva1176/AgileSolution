using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgileTask.Domain.Contracts.Services
{
    public interface IService<T, TId>
    {
        Task AddNew(T item);
        Task<T> GetById(TId id);
        Task<IEnumerable<T>> GetALL();
        Task Remove(T item);
        Task Update(T item);
        Task<int> Count();
    }
}