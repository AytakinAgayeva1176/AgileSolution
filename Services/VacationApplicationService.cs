using AgileTask.Domain.Contracts.Services;
using AgileTask.Domain.Entities;
using AgileTask.Domain.ViewModels;
using AgileTask.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgileTask.Services
{
    public class VacationApplicationService : IVacationApplicationService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;
        public VacationApplicationService(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
        }

        public async Task AddNew(VacationApplicationViewModel item)
        {
            var entity = _mapper.Map<VacationApplication>(item);
            entity.CreatedDate = DateTime.Now;
            await _repositoryFactory.Repository.Add(entity);
        }

        public async Task<int> Count()
        {
            return await _repositoryFactory.Repository.Count<Department>();
        }
        public async Task<IEnumerable<VacationApplicationViewModel>> GetALL()
        {
            var VacationApplications = await _repositoryFactory.Repository.List<VacationApplication>();
            return _mapper.Map<List<VacationApplicationViewModel>>(VacationApplications);
        }

        public async Task<VacationApplicationViewModel> GetById(int id)
        {
            var entity = await _repositoryFactory.Repository.GetById<VacationApplication>(id);
            return _mapper.Map<VacationApplicationViewModel>(entity);
        }

        public async Task Remove(VacationApplicationViewModel item)
        {
            var entity = _mapper.Map<VacationApplication>(item);
            await _repositoryFactory.Repository.Delete<VacationApplication>(entity);
        }


        public async Task Update(VacationApplicationViewModel item)
        {
            var entity = await _repositoryFactory.Repository.GetById<VacationApplication>(item.Id);
            entity.StartDate = item.StartDate;
            entity.Status = item.Status;
            entity.Days = item.Days;
            entity.UpdatedDate = DateTime.Now;
            await _repositoryFactory.Repository.Update<VacationApplication>(entity);
        }


    }
}
