using AgileTask.Domain.Contracts.Services;
using AgileTask.Domain.Entities;
using AgileTask.Domain.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgileTask.Services
{

    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(IMapper mapper, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<string> Create(CreateUserVM model)
        {
            var user = _mapper.Map<ApplicationUser>(model);
             user.UserName = model.Email;

             await _userManager.CreateAsync(user, model.Password);
            var res = user.Id;
            return res;
        }

        public async Task<int> Count()
        {
            return await _userManager.Users.CountAsync();
        }
        public async Task<List<UserViewModel>> GetAll()
        {
            var users = await _userManager.Users.Include(x=>x.Position).Include(x => x.Department).ToListAsync();
            return _mapper.Map<List<UserViewModel>>(users);
        }

        public async Task<UserViewModel> GetById(string id)
        {
            var user = await _userManager.Users.Include(x => x.VacationApplications).FirstOrDefaultAsync(u => u.Id == id);
            var apps = _mapper.Map<List<VacationApplicationViewModel>>(user.VacationApplications);
            var userVm= _mapper.Map<UserViewModel>(user);
            userVm.Applications = apps;
            return userVm;
        }

        public async Task<UserViewModel> FindUser(LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            return _mapper.Map<UserViewModel>(user);
        }
       
        public async Task<SignInResult> Login(LoginViewModel loginViewModel)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(
                loginViewModel.Email, loginViewModel.Password, false, false);

            return signInResult;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }


            public async Task<IdentityResult> Update(UserViewModel viewModel)
        {
            var user = await _userManager.FindByIdAsync(viewModel.Id);
            var uptatedUser = _mapper.Map(viewModel, user);
            uptatedUser.UserName = viewModel.Email;
            var newPassword = _userManager.PasswordHasher.HashPassword(uptatedUser, viewModel.Password); 
            uptatedUser.PasswordHash = newPassword;
            var result = await _userManager.UpdateAsync(uptatedUser);

            return result;
        }

        public async Task AddRoleToUser(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
             await _userManager.AddToRoleAsync(user, roleName);
        }
    }
}

