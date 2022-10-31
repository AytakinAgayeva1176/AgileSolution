using AgileTask.Domain.Contracts.Services;
using AgileTask.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AgileTask.Controllers
{
    public class AccountController : Controller
    {
        #region fields
        private readonly IUserService _userService;
        #endregion

        #region ctor
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {

            if (ModelState.IsValid)
            {
                var user = await _userService.FindUser(loginViewModel);
                if (user != null)
                {

                    var result = await _userService.Login(loginViewModel);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    else  ModelState.AddModelError("", "Email ve ya şifrə yanlışdır"); 
                }

                else ModelState.AddModelError("", "Istifadəçi tapılmadı");
            }


            return View(loginViewModel);
        }

        public async Task<IActionResult> LogOut()
        {
            await _userService.LogOut();

            return RedirectToAction("Login", "Account");
        }


        [HttpGet]
        [Route("/Account/AccessDenied")]
        public ActionResult AccessDenied()
        {
            return View();
        }


    }
}
