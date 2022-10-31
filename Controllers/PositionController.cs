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
    public class PositionController : Controller
    {
        #region This Ctor

        readonly IServiceFactory _serviceFactory;
        public PositionController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;

        }
        #endregion

        #region Interfaces
        IPositionService PositionService { get; set; }
        IDepartmentService DepartmentService { get; set; }
        #endregion


        public async Task<IActionResult> Index()
        {
            PositionService = _serviceFactory.PositionService;
            var Positions = await PositionService.GetALL();
            return View(Positions);
        }
        public async Task<IActionResult> AddNewPosition()
        {
            DepartmentService = _serviceFactory.DepartmentService;
            var departments = await DepartmentService.GetALL();
            Dictionary<int, string> departmentList = new Dictionary<int, string>();
            foreach (var item in departments)
            {
                departmentList[item.Id] = item.AbbrName+" - "+ item.Name;
            }
            ViewData["Departments"] = new SelectList(departmentList, "Key", "Value");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewPosition(PositionViewModel model)
        {

            if (ModelState.IsValid)
            {
                PositionService = _serviceFactory.PositionService;
                await PositionService.AddNew(model);
                var result = await _serviceFactory.SaveAsync()>0;
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Emeliyyat ugursuzdur!");
                }
            }

            DepartmentService = _serviceFactory.DepartmentService;
            var departments = await DepartmentService.GetALL();
            Dictionary<int, string> departmentList = new Dictionary<int, string>();
            foreach (var item in departments)
            {
                departmentList[item.Id] = item.AbbrName + " - " + item.Name;
            }
            ViewData["Departments"] = new SelectList(departmentList, "Key", "Value");

            return View(model);
        }




        public async Task<IActionResult> Edit(int id)
        {
            PositionService = _serviceFactory.PositionService;
            var days = await PositionService.GetById(id);
            DepartmentService = _serviceFactory.DepartmentService;
            var departments = await DepartmentService.GetALL();
            Dictionary<int, string> departmentList = new Dictionary<int, string>();
            foreach (var item in departments)
            {
                departmentList[item.Id] = item.AbbrName + " - " + item.Name;
            }
            ViewData["Departments"] = new SelectList(departmentList, "Key", "Value");
            return View(days);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(PositionViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                PositionService = _serviceFactory.PositionService;
                await PositionService.Update(viewModel);
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
            DepartmentService = _serviceFactory.DepartmentService;
            var departments = await DepartmentService.GetALL();
            Dictionary<int, string> departmentList = new Dictionary<int, string>();
            foreach (var item in departments)
            {
                departmentList[item.Id] = item.AbbrName + " - " + item.Name;
            }
            ViewData["Departments"] = new SelectList(departmentList, "Key", "Value");
            return View(viewModel);
        }


    }
}
