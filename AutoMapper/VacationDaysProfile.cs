using AgileTask.Domain.Entities;
using AgileTask.Domain.ViewModels;
using AutoMapper;

namespace AgileTask.AutoMapper
{
    public class VacationDaysProfile : Profile
    {
        public VacationDaysProfile()
        {
            CreateMap<VacationDay, VacationDayViewModel>();
            CreateMap<VacationDayViewModel, VacationDay>();
        }
    }
}