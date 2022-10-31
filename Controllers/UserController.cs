using AgileTask.Domain.Contracts.Services;
using AgileTask.Domain.ViewModels;
using AgileTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgileTask.Controllers
{

    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        readonly IServiceFactory _serviceFactory;
        public UserController(IUserService userService, IRoleService roleService, IServiceFactory serviceFactory)
        {
            _userService = userService;
            _roleService = roleService;
            _serviceFactory = serviceFactory;
        }

        #region Interfaces
        IDepartmentService DepartmentService { get; set; }
        IPositionService PositionService { get; set; }
        #endregion


        public async Task<IActionResult> Index()
        {
            var users =await _userService.GetAll();
            return View(users);
        }

        public async Task<IActionResult> Create()
        {
            #region ViewData

            PositionService = _serviceFactory.PositionService;
            DepartmentService = _serviceFactory.DepartmentService;

            var departmentList = await DepartmentService.GetALL();
            var positionList = await PositionService.GetALL();
            var roleList = await _roleService.GetAll();


            Dictionary<string, string> roles = new Dictionary<string, string>();
            Dictionary<int, string> departments = new Dictionary<int, string>();
            Dictionary<int, string> positions = new Dictionary<int, string>();


            foreach (var item in roleList)
            {
                roles[item.Id] = item.Name;
            }
            foreach (var item in departmentList)
            {
                departments[item.Id] = item.Name;
            }
            foreach (var item in positionList)
            {
                positions[item.Id] = item.Name;
            }


            ViewData["Roles"] = new SelectList(roles, "Key", "Value");
            ViewData["Positions"] = new SelectList(positions, "Key", "Value");
            ViewData["Departments"] = new SelectList(departments, "Key", "Value");

            #endregion


            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(CreateUserVM viewModel)
        {

            if (ModelState.IsValid)
            {
                var userId = await _userService.Create(viewModel);

                if (viewModel.RoleIds!=null)
                {
                    foreach (var id in viewModel.RoleIds)
                    {
                        var rolename = await _roleService.GetById(id);
                        if (rolename != null)
                        {
                            await _userService.AddRoleToUser(userId, rolename.Name);
                        }
                        else ModelState.AddModelError("", "Role tapılmadı");
                    }

                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Istifadəçi yaradılmadı");
                }

            }
            #region ViewData

            PositionService = _serviceFactory.PositionService;
            DepartmentService = _serviceFactory.DepartmentService;

            var departmentList = await DepartmentService.GetALL();
            var positionList = await PositionService.GetALL();
            var roleList = await _roleService.GetAll();


            Dictionary<string, string> roles = new Dictionary<string, string>();
            Dictionary<int, string> departments = new Dictionary<int, string>();
            Dictionary<int, string> positions = new Dictionary<int, string>();


            foreach (var item in roleList)
            {
                roles[item.Id] = item.Name;
            }
            foreach (var item in departmentList)
            {
                departments[item.Id] = item.Name;
            }
            foreach (var item in positionList)
            {
                positions[item.Id] = item.Name;
            }


            ViewData["Roles"] = new SelectList(roles, "Key", "Value");
            ViewData["Positions"] = new SelectList(positions, "Key", "Value");
            ViewData["Departments"] = new SelectList(departments, "Key", "Value");

            #endregion
            return View(viewModel);
    }


        public async Task<IActionResult> Details(string id)
        {
            var user =await _userService.GetById(id);
            return View(user);
        }


        public async Task<IActionResult> Settings(string id)
        {
            #region ViewData

            PositionService = _serviceFactory.PositionService;
            DepartmentService = _serviceFactory.DepartmentService;

            var departmentList = await DepartmentService.GetALL();
            var positionList = await PositionService.GetALL();

            Dictionary<int, string> departments = new Dictionary<int, string>();
            Dictionary<int, string> positions = new Dictionary<int, string>();

            foreach (var item in departmentList)
            {
                departments[item.Id] = item.Name;
            }
            foreach (var item in positionList)
            {
                positions[item.Id] = item.Name;
            }


            ViewData["Positions"] = new SelectList(positions, "Key", "Value");
            ViewData["Departments"] = new SelectList(departments, "Key", "Value");

            #endregion
            var user =await _userService.GetById(id);
            return View(user);
        }

        [HttpPost]

        public async Task<IActionResult> Settings(UserViewModel viewModel)
        {
           
            if (ModelState.IsValid)
            {
                var result = await _userService.Update(viewModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "User");
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Əməliyyat yerinə yetirilmədi");
                }

            }
            #region ViewData

            PositionService = _serviceFactory.PositionService;
            DepartmentService = _serviceFactory.DepartmentService;

            var departmentList = await DepartmentService.GetALL();
            var positionList = await PositionService.GetALL();


            Dictionary<int, string> departments = new Dictionary<int, string>();
            Dictionary<int, string> positions = new Dictionary<int, string>();


            foreach (var item in departmentList)
            {
                departments[item.Id] = item.Name;
            }
            foreach (var item in positionList)
            {
                positions[item.Id] = item.Name;
            }


            ViewData["Positions"] = new SelectList(positions, "Key", "Value");
            ViewData["Departments"] = new SelectList(departments, "Key", "Value");

            #endregion
            return View(viewModel);
        }
    }
}
