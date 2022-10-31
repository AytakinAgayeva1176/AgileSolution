using AgileTask.Domain.Contracts.Services;
using AgileTask.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AgileTask.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        #region fields
        private readonly IRoleService _roleService;
        #endregion

        #region ctor
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        #endregion


        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAll();
            return View(roles);
        }
        public IActionResult CreateRole()
        {
            
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
               
                var result = await _roleService.Create(roleViewModel);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Role");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Emeliyyat ugursuzdur!");
                }

            }

 
            return View(roleViewModel);
        }

    }

}

