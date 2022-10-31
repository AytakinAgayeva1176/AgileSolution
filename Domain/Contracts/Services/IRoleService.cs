using AgileTask.Domain.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgileTask.Domain.Contracts.Services
{
    public interface IRoleService
    {
        Task<List<RoleViewModel>> GetAll();
        Task<RoleViewModel> GetById(string id);
        Task<IdentityResult> Create(RoleViewModel roleViewModel);
        Task<IList<string>> GetUserRole(string userId);
    }
}
