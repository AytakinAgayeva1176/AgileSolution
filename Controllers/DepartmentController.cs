using AgileTask.Domain.Contracts.Services;
using AgileTask.Domain.ViewModels;
using AgileTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AgileTask.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartmentController : Controller
    {
        #region This Ctor

        readonly IServiceFactory _serviceFactory;
        public DepartmentController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;

        }
        #endregion


        #region Interfaces
        IDepartmentService DepartmentService { get; set; }
        #endregion


        public async Task<IActionResult> Index()
        {
            DepartmentService = _serviceFactory.DepartmentService;
            var departments = await DepartmentService.GetALL();
            return View(departments);
        }
        public IActionResult AddNewDepartment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewDepartment(DepartmentViewModel model)
        {

            if (ModelState.IsValid)
            {
                DepartmentService = _serviceFactory.DepartmentService;
                await DepartmentService.AddNew(model);
                var result = await _serviceFactory.SaveAsync() > 0;
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Emeliyyat ugursuzdur!");
                }
            }

            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            DepartmentService = _serviceFactory.DepartmentService;
            var days = await DepartmentService.GetById(id);

            return View(days);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(DepartmentViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                DepartmentService = _serviceFactory.DepartmentService;
                await DepartmentService.Update(viewModel);
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

    }
}
