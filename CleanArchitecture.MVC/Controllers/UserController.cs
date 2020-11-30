﻿using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Application.Model;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Threading.Tasks;

namespace CleanArchitecture.MVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IUserService userService, IToastNotification toastNotification, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _toastNotification = toastNotification;
            _userManager = userManager;
        }

        [Authorize(Roles = AppConst.Role.AdminRole)]
        public async Task<IActionResult> Index()
        {
            var model = new UserListViewModel
            {
                Users = await _userService.GetAll()
            };
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (id != null)
            {
                var modelDb = await _userService.Get(userId: id, currentUserId: currentUser.Id);
                return View(modelDb);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]        
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            await _userService.Update(model);
            _toastNotification.AddSuccessToastMessage("The user has been updated");
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = AppConst.Role.AdminRole)]
        public async Task<IActionResult> AcivateUser(string id)
        {
            await _userService.ActivateUser(id);
            _toastNotification.AddSuccessToastMessage("The user has been activated");
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = AppConst.Role.AdminRole)]
        public async Task<IActionResult> DeactivateUser(string id)
        {
            await _userService.DeactivateUser(id);
            _toastNotification.AddSuccessToastMessage("The user has been deactivated");
            return RedirectToAction(nameof(Index));
        }
    }
}
