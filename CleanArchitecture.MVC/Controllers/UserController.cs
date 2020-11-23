using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Application.Model;
using CleanArchitecture.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Threading.Tasks;

namespace CleanArchitecture.MVC.Controllers
{
    [Authorize(Roles = AppConst.Role.AdminRole)]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IToastNotification _toastNotification;

        public UserController(IUserService userService, IToastNotification toastNotification)
        {
            _userService = userService;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var model = new UserListViewModel
            {
                Users = await _userService.GetAll()
            };
            return View(model);
        }

        public async Task<IActionResult> AcivateUser(string id)
        {
            if (id != null)
            {
                await _userService.ActivateUser(id);
                _toastNotification.AddSuccessToastMessage("The user has been activated");                
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeactivateUser(string id)
        {
            if (id != null)
            {
                await _userService.DeactivateUser(id);
                _toastNotification.AddSuccessToastMessage("The user has been deactivated");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
