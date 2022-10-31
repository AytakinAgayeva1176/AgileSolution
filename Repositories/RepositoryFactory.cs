using AgileTask.Domain.Contexts;
using AgileTask.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileTask.Repositories
{
    public interface IRepositoryFactory
    {
        public IRepository Repository { get; }
        Task<int> SaveAsync();
    }
    public class RepositoryFactory : IDisposable, IRepositoryFactory
    {
        private readonly AgileDbContext _context;

        public RepositoryFactory(AgileDbContext context, IRepository rep)
        {
            _context = context;
            Repository = rep;
        }



        public IRepository Repository { get; }


        #region SaveChange
        public async Task<int> SaveAsync()
        {

            int result = await _context.SaveChangesAsync();

            return result;


        }
        #endregion

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
