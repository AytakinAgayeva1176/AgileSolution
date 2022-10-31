using AgileTask.Domain.Contracts.Services;
using AgileTask.Domain.Entities;
using AgileTask.Domain.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgileTask.Services
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public RoleService(IMapper mapper, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IdentityResult> Create(RoleViewModel roleViewModel)
        {
            roleViewModel.Id = Guid.NewGuid().ToString();
            var role = _mapper.Map<IdentityRole>(roleViewModel);
            return await _roleManager.CreateAsync(role);
        }

        public async Task<List<RoleViewModel>> GetAll()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return _mapper.Map<List<RoleViewModel>>(roles);
        }

        public async Task<RoleViewModel> GetById(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return _mapper.Map<RoleViewModel>(role);
        }

        public async Task<IList<string>> GetUserRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }
    }
}
