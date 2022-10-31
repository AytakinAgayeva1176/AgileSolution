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
    [Authorize(Roles ="Admin, HR")]
    public class VacationDayController : Controller
    {
        #region This Ctor

        readonly IServiceFactory _serviceFactory;
        public VacationDayController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;

        }
        #endregion


        #region Interfaces
        IVacationDaysService VacationDayService { get; set; }
        IPositionService PositionService { get; set; }
        #endregion


        public async Task<IActionResult> Index()
        {
            VacationDayService = _serviceFactory.VacationDaysService;
            var vacationDays = await VacationDayService.GetALL();
            return View(vacationDays);
        }
        public IActionResult AddNewVacationDay()
        {
            PositionService = _serviceFactory.PositionService;
            var positionList = PositionService.GetALL().Result;

            Dictionary<int, string> positions = new Dictionary<int, string>();
            foreach (var item in positionList)
            {
                positions[item.Id] = item.Name;
            }
            ViewData["Positions"] = new SelectList(positions, "Key", "Value");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewVacationDay(VacationDayViewModel model)
        {

            if (ModelState.IsValid)
            {
                VacationDayService = _serviceFactory.VacationDaysService;
                await VacationDayService.AddNew(model);
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

            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            VacationDayService = _serviceFactory.VacationDaysService;
            var days = await VacationDayService.GetById(id);

            #region Position ViewData
            PositionService = _serviceFactory.PositionService;
            var positionList = PositionService.GetALL().Result;

            Dictionary<int, string> positions = new Dictionary<int, string>();
            foreach (var item in positionList)
            {
                positions[item.Id] = item.Name;
            }
            ViewData["Positions"] = new SelectList(positions, "Key", "Value");
            #endregion

            return View(days);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(VacationDayViewModel viewModel)
        {
           
            if (ModelState.IsValid)
            {
                VacationDayService = _serviceFactory.VacationDaysService;
                await VacationDayService.Update(viewModel);
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

            #region Position ViewData
            PositionService = _serviceFactory.PositionService;
            var positionList = PositionService.GetALL().Result;

            Dictionary<int, string> positions = new Dictionary<int, string>();
            foreach (var item in positionList)
            {
                positions[item.Id] = item.Name;
            }
            ViewData["Positions"] = new SelectList(positions, "Key", "Value");
            #endregion
            return View(viewModel);
        }
    }
}
