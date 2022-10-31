
using AgileTask.Domain.Contracts.Services;
using AgileTask.Domain.ViewModels;
using AgileTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AgileTask.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region This Ctor

        readonly IServiceFactory _serviceFactory;
        readonly IUserService _userService;
        public HomeController(IServiceFactory serviceFactory,IUserService userService)
        {
            _serviceFactory = serviceFactory;
            _userService = userService;
        }
        #endregion


        #region Interfaces
        IVacationApplicationService VacationApplicationService { get; set; }
        IDepartmentService DepartmentService { get; set; }
        IPositionService PositionService { get; set; }
        #endregion


        public async Task<IActionResult> Index()
        {
            DepartmentService = _serviceFactory.DepartmentService;
            PositionService = _serviceFactory.PositionService;
            VacationApplicationService = _serviceFactory.VacationApplicationService;
            DashboardVM dashboardVM = new DashboardVM()
            {
                DepartmentsCount = await DepartmentService.Count(),
                PositionsCount = await PositionService.Count(),
                ApplicationsCount = await VacationApplicationService.Count(),
                UsersCount = await _userService.Count()

            };
            return View(dashboardVM);
        }



    }
}
