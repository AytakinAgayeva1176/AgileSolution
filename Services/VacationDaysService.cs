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
    public class VacationDaysService : IVacationDaysService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;
        public VacationDaysService(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
        }

        public async Task AddNew(VacationDayViewModel item)
        {
            var entity = _mapper.Map<VacationDay>(item);
            entity.CreatedDate = DateTime.Now;
            await _repositoryFactory.Repository.Add(entity);
        }

        public async Task<int> Count()
        {
            return await _repositoryFactory.Repository.Count<Department>();
        }
        public async Task<IEnumerable<VacationDayViewModel>> GetALL()
        {
            var VacationDays = await _repositoryFactory.Repository.List<VacationDay>();
            return _mapper.Map<List<VacationDayViewModel>>(VacationDays);
        }

        public async Task<VacationDayViewModel> GetById(int id)
        {
            var entity = await _repositoryFactory.Repository.GetById<VacationDay>(id);
            return _mapper.Map<VacationDayViewModel>(entity);
        }

        public async Task Remove(VacationDayViewModel item)
        {
            var entity = _mapper.Map<VacationDay>(item);
            await _repositoryFactory.Repository.Delete<VacationDay>(entity);
        }


        public async Task Update(VacationDayViewModel item)
        {
            var entity = _mapper.Map<VacationDay>(item);
            entity.UpdatedDate = DateTime.Now;
            await _repositoryFactory.Repository.Update<VacationDay>(entity);
        }


    }
}
