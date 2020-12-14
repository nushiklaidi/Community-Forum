using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Application.Model;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.MVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
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

        [Authorize(Roles = AppConst.Role.AdminRole)]
        public async Task<IActionResult> GetUsers()
        {
            var model = new UserListViewModel
            {
                Users = await _userService.GetAll()
            };
            return PartialView("_UsersTable", model);
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
            try
            {
                if (ModelState.IsValid)
                {
                    await _userService.Update(model);
                    return StatusCode(200);
                }
                else
                {
                    string errors = "";
                    errors = string.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                    return StatusCode(400, errors);
                }                
            }
            catch (Exception ex)
            {

                return StatusCode(400, ex.Message);
            }
        }

        [Authorize(Roles = AppConst.Role.AdminRole)]
        [HttpPost]
        public async Task<IActionResult> ActivateUser(string id)
        {
            try
            {
                await _userService.ActivateUser(id);
                return StatusCode(200);
            }
            catch (Exception ex)
            {

                return StatusCode(400, ex.Message);
            }            
        }

        [Authorize(Roles = AppConst.Role.AdminRole)]
        [HttpPost]
        public async Task<IActionResult> DeactivateUser(string id)
        {
            try
            {
                await _userService.DeactivateUser(id);
                return StatusCode(200);
            }
            catch (Exception ex)
            {

                return StatusCode(400, ex.Message);
            }
        }
    }
}
