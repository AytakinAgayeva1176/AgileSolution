using AgileTask.Domain.Entities;
using AgileTask.Domain.ViewModels;
using System;

namespace AgileTask.Domain.Contracts.Services
{
    public interface IVacationApplicationService : IService<VacationApplicationViewModel, int>
    {
    }
}
