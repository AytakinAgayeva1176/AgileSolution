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
    public class PositionService : IPositionService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;
        public PositionService(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
        }

        public async Task AddNew(PositionViewModel item)
        {
            var entity = _mapper.Map<Position>(item);
            entity.CreatedDate = DateTime.Now;
            await _repositoryFactory.Repository.Add(entity);
        }

        public async Task<int> Count()
        {
            return await _repositoryFactory.Repository.Count<Position>();
        }
        public async Task<IEnumerable<PositionViewModel>> GetALL()
        {
            var Positions = await _repositoryFactory.Repository.List<Position>();
            return _mapper.Map<List<PositionViewModel>>(Positions);
        }

        public async Task<PositionViewModel> GetById(int id)
        {
            var entity = await _repositoryFactory.Repository.GetById<Position>(id);
            return _mapper.Map<PositionViewModel>(entity);
        }

        public async Task Remove(PositionViewModel item)
        {
            var entity = _mapper.Map<Position>(item);
            await _repositoryFactory.Repository.Delete<Position>(entity);
        }


        public async Task Update(PositionViewModel item)
        {
            var entity = _mapper.Map<Position>(item);
            entity.UpdatedDate = DateTime.Now;
            await _repositoryFactory.Repository.Update<Position>(entity);
        }


    }
}
