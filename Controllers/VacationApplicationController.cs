using AgileTask.Domain.Contracts.Services;
using AgileTask.Domain.Enums;
using AgileTask.Domain.ViewModels;
using AgileTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AgileTask.Controllers
{
    [Authorize]
    public class VacationApplicationController : Controller
    {
        #region This Ctor

        readonly IServiceFactory _serviceFactory;
        readonly IUserService _userService;
        readonly IRoleService _roleService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public VacationApplicationController(
            IServiceFactory serviceFactory,
            IUserService userService,
            IRoleService roleService,
            IHttpContextAccessor httpContextAccessor)
        {
            _serviceFactory = serviceFactory;
            _userService = userService;
            _roleService = roleService;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion


        #region Interfaces
        IVacationApplicationService VacationApplicationService { get; set; }
        #endregion


        public async Task<IActionResult> Index()
        {
            IEnumerable<VacationApplicationViewModel> applications = new List<VacationApplicationViewModel>();


            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var roles = await _roleService.GetUserRole(userId);
            if (roles.Count != 0)
            {
                foreach (var item in roles)
                {
                    if (item == "User")
                    {
                        var user = await _userService.GetById(userId);
                        applications = user.Applications;
                    }
                    else
                    {
                        VacationApplicationService = _serviceFactory.VacationApplicationService;
                        applications = await VacationApplicationService.GetALL();
                    }
                }
            }

            return View(applications);
        }


        public IActionResult AddNewVacationApplication()
        {

            var userList = _userService.GetAll().Result;

            Dictionary<string, string> users = new Dictionary<string, string>();
            foreach (var item in userList)
            {
                users[item.Id] = item.Name + "" + item.Surname;
            }
            ViewData["Users"] = new SelectList(users, "Key", "Value");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewVacationApplication(VacationApplicationViewModel model)
        {

            if (ModelState.IsValid)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                VacationApplicationService = _serviceFactory.VacationApplicationService;
                model.UserId = userId;
                await VacationApplicationService.AddNew(model);
                var result = await _serviceFactory.SaveAsync() > 0;
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception(message: "Something went wrong");
                }
            }

            return View(model);
        }




        public async Task<IActionResult> Edit(int id)
        {
            VacationApplicationService = _serviceFactory.VacationApplicationService;
            var days = await VacationApplicationService.GetById(id);
            if (days.Status == Status.Pending)
            {
                return View(days);
            }
            else return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> Edit(VacationApplicationViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                VacationApplicationService = _serviceFactory.VacationApplicationService;
                await VacationApplicationService.Update(viewModel);
                var result = await _serviceFactory.SaveAsync() > 0;
                if (result)
                {
                    return RedirectToAction("Index");
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Əməliyyat yerinə yetirilmədi");
                }
            }

            return View(viewModel);
        }




        [Authorize(Roles = "HR")]

        public async Task<IActionResult> ChangeApplicationStatus(int Id, string status)
        {
            VacationApplicationService = _serviceFactory.VacationApplicationService;
            var application = await VacationApplicationService.GetById(Id);
            if (status == "accepted")
            {
                application.Status = Status.Accepted;
            }
            else if (status == "rejected")
            {
                application.Status = Status.Rejected;
            }

            await VacationApplicationService.Update(application);
            await _serviceFactory.SaveAsync();

            return RedirectToAction("Index");
        }



    }
}
