using AgileTask.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgileTask.Domain.Contracts.Repositories
{
    public interface IRepository
    {
        Task<T> GetById<T>(int id) where T : BaseEntity;
        Task<T> GetById<T>(int id, string include) where T : BaseEntity;
        Task<List<T>> List<T>() where T : BaseEntity;

        Task<T> Add<T>(T entity) where T : BaseEntity;
        Task Update<T>(T entity) where T : BaseEntity;
        Task Delete<T>(T entity) where T : BaseEntity;
        Task<int> Count<T>() where T : BaseEntity;
        Task SaveChange();
    }
}
