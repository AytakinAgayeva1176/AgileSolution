using AgileTask.Domain.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgileTask.Domain.Contracts.Services
{
    public interface IUserService 
    {
        Task<string> Create(CreateUserVM viewModel);
        Task AddRoleToUser(string userId, string roleName);
        Task<UserViewModel> GetById(string id) ;
        Task<UserViewModel> FindUser(LoginViewModel loginViewModel);
        Task<List<UserViewModel>> GetAll();
        Task<IdentityResult> Update(UserViewModel viewModel);
        Task<SignInResult> Login(LoginViewModel loginViewModel);
        Task LogOut();
        Task<int> Count();
    }
}
