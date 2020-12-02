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
            var model = new ForumListModel
            {
                ForumList = _forumService.GetAll()
            };
            return View(model);
        }

        [Authorize(Roles = AppConst.Role.AdminRole)]
        public IActionResult Create()
        {
            var model = new ForumViewModel();
            return View(model);
        }

        [Authorize(Roles = AppConst.Role.AdminRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddForum(ForumViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _forumService.Save(model: model);
                _toastNotification.AddSuccessToastMessage("The forum has been created");
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Create), model);
        }

        [Authorize(Roles = AppConst.Role.AdminRole)]
        public IActionResult Edit(int id)
        {
            var modelDb = _forumService.GetById(forumId: id);
            var model = new ForumViewModel()
            {
               Id = modelDb.Id,
               Title = modelDb.Title,
               Description = modelDb.Description
            };
            return View(model);
        }

        [Authorize(Roles = AppConst.Role.AdminRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ForumViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _forumService.Save(model: model);
                _toastNotification.AddSuccessToastMessage("The forum has been updated");
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Edit), model);
        }
    }
}
