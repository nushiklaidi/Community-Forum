using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Application.Model;
using CleanArchitecture.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Threading.Tasks;

namespace CleanArchitecture.MVC.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        private readonly IForumService _forumService;
        private readonly IToastNotification _toastNotification;

        public ForumController(IForumService forumService, IToastNotification toastNotification)
        {
            _forumService = forumService;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            var model = new ForumViewModel
            {
                ForumList = _forumService.GetAll()
            };
            return View(model);
        }

        [Authorize(Roles = AppConst.Role.AdminRole)]
        public IActionResult Create()
        {
            var model = new ForumAddViewModel();
            return View(model);
        }

        [Authorize(Roles = AppConst.Role.AdminRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddForum(ForumAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _forumService.Create(model);
                _toastNotification.AddSuccessToastMessage("The forum has been created");
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Create), model);
        }
    }
}
