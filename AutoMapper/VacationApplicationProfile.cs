using AgileTask.Domain.Entities;
using AgileTask.Domain.ViewModels;
using AutoMapper;

namespace AgileTask.AutoMapper
{
    public class VacationApplicationProfile : Profile
    {
        public VacationApplicationProfile()
        {
            CreateMap<VacationApplication, VacationApplicationViewModel>();
            CreateMap<VacationApplicationViewModel, VacationApplication>();
        }
    }
}