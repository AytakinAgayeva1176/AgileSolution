using AgileTask.Repositories;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace AgileTask.Services
{
    public interface IServiceFactory
    {

        public PositionService PositionService { get; }
        public DepartmentService DepartmentService { get; }
        public VacationApplicationService VacationApplicationService { get; }
        public VacationDaysService VacationDaysService { get; }

        Task<int> SaveAsync();
    }
    public class ServiceFactory: IServiceFactory,IDisposable
    {

        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;
        public ServiceFactory(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
        }

       
        private PositionService _positionService;
        public PositionService PositionService
        {
            get
            {
                this._positionService ??= new PositionService(_repositoryFactory, _mapper);
                return _positionService;
            }
        }

        private DepartmentService _departmentService;
        public DepartmentService DepartmentService
        {
            get
            {
                this._departmentService ??= new DepartmentService(_repositoryFactory, _mapper);
                return _departmentService;
            }
        }

        private VacationApplicationService _vacationApplicationService;
        public VacationApplicationService VacationApplicationService
        {
            get
            {
                this._vacationApplicationService ??= new VacationApplicationService(_repositoryFactory, _mapper);
                return _vacationApplicationService;
            }
        }

      
        private VacationDaysService _vacationDaysService;
        public VacationDaysService VacationDaysService
        {
            get
            {
                this._vacationDaysService ??= new VacationDaysService(_repositoryFactory, _mapper);
                return _vacationDaysService;
            }
        }





        #region SaveChange
        public async Task<int> SaveAsync()
        {
            return await _repositoryFactory.SaveAsync();
        }
        #endregion

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    ((IDisposable)_repositoryFactory).Dispose();
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
