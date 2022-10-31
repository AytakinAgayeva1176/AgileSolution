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
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;
        public DepartmentService(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
        }

        public async Task AddNew(DepartmentViewModel item)
        {
            var entity = _mapper.Map<Department>(item);
            entity.CreatedDate = DateTime.Now;
             await _repositoryFactory.Repository.Add(entity);
        }

        public async Task<int> Count()
        {
            return await _repositoryFactory.Repository.Count<Department>();
        }

        public async Task<IEnumerable<DepartmentViewModel>> GetALL()
        {
           var departments = await _repositoryFactory.Repository.List<Department>();
            return _mapper.Map<List<DepartmentViewModel>>(departments);
        }

        public async Task<DepartmentViewModel> GetById(int id)
        {
            var entity= await _repositoryFactory.Repository.GetById<Department>(id);
            return _mapper.Map<DepartmentViewModel>(entity);
        }

        public async Task Remove(DepartmentViewModel item)
        {
            var entity = _mapper.Map<Department>(item);
            await _repositoryFactory.Repository.Delete<Department>(entity);
        }


        public async Task Update(DepartmentViewModel item)
        {
            var entity = _mapper.Map<Department>(item);
            entity.UpdatedDate = DateTime.Now;
            await _repositoryFactory.Repository.Update<Department>(entity);
        }

    
    }
}
