using AgileTask.Domain.Entities;
using AgileTask.Domain.ViewModels;
using AutoMapper;

namespace AgileTask.AutoMapper
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentViewModel>();
            CreateMap<DepartmentViewModel, Department>();
        }
    }
}