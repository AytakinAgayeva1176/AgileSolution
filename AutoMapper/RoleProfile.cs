using AgileTask.Domain.Entities;
using AgileTask.Domain.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AgileTask.AutoMapper
{
    public class RoleProfile :Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleViewModel>().ReverseMap();
        }
    }
}
