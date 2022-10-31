using AgileTask.Domain.Contexts;
using AgileTask.Domain.Contracts.Repositories;
using AgileTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgileTask.Repositories
{
    public class EfRepository : IRepository
    {
        private readonly AgileDbContext _dbContext;

        public EfRepository(AgileDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> GetById<T>(int id) where T : BaseEntity
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> GetById<T>(int id, string include) where T : BaseEntity
        {
            return await _dbContext.Set<T>()
                .Include(include)
                .SingleOrDefaultAsync(e => e.Id == id);
        }


        public async Task<List<T>> List<T>() where T : BaseEntity
        {
           
            if (typeof(T) == typeof(VacationApplication))
            {
                var items = _dbContext.VacationApplications.Include(x => x.User);
                var res = await items.ToListAsync() as List<T>;
                return res;
            }
            if (typeof(T) == typeof(VacationDay))
            {
                var items = _dbContext.VacationDays.Include(x => x.Position);
                var res = await items.ToListAsync() as List<T>;
                return res;
            }

            var query = _dbContext.Set<T>().AsQueryable();
            return await query.ToListAsync();
        }

        public async Task<T> Add<T>(T entity) where T : BaseEntity
        {
             _dbContext.Set<T>().Add(entity) ;
            return entity;
        }

        public async Task Delete<T>(T entity) where T : BaseEntity
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task Update<T>(T entity) where T : BaseEntity
        {
           _dbContext.Entry(entity).State = EntityState.Modified;
            //await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Count<T>() where T : BaseEntity
        {
            return await _dbContext.Set<T>().AsQueryable().CountAsync();
        }

        public async Task SaveChange()
        {
            await _dbContext.SaveChangesAsync();
        }



    }
}
