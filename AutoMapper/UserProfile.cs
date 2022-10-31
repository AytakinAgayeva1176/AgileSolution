using AgileTask.Domain.Entities;
using AgileTask.Domain.ViewModels;
using AutoMapper;

namespace AgileTask.AutoMapper
{
    public class UserProfile :Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>();
            CreateMap<UserViewModel, ApplicationUser>();
            CreateMap<CreateUserVM, ApplicationUser>().ReverseMap();
        }
        
    }
}
