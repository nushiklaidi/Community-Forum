using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Application.Model;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.MVC.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        private readonly IForumService _forumService;
        private readonly IPostService _postService;
        private readonly IToastNotification _toastNotification;

        public ForumController(IForumService forumService, IToastNotification toastNotification, IPostService postService)
        {
            _forumService = forumService;
            _toastNotification = toastNotification;
            _postService = postService;
        }

        public IActionResult Index()
        {
            var model = new ForumListModel
            {
                ForumList = _forumService.GetAll()
            };
            return View(model);
        }

        public IActionResult GetForums()
        {
            var model = new ForumListModel
            {
                ForumList = _forumService.GetAll()
            };
            return PartialView("_ForumsTable", model);
        }

        [HttpGet]
        public IActionResult ForumDetails(int id)
        {
            var forum = _forumService.GetById(forumId: id);
            var model = new ForumDetailsViewModel
            {
                Posts = _postService.GetPostsByForum(model: forum),
                Forum = _forumService.BuildForum(model: forum)
            };
            return View(model);
        }

        public IActionResult GetForumDetails(int id)
        {
            var forum = _forumService.GetById(forumId: id);
            var model = new ForumDetailsViewModel
            {
                Posts = _postService.GetPostsByForum(model: forum),
                Forum = _forumService.BuildForum(model: forum)
            };
            return PartialView("_ForumDetailsTable", model);
        }

        [Authorize(Roles = AppConst.Role.AdminRole)]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                var model = new ForumViewModel();
                return View(model);
            }
            else
            {
                var modelDb = _forumService.GetById(forumId: id);
                var model = new ForumViewModel()
                {
                    Id = modelDb.Id,
                    Title = modelDb.Title,
                    Description = modelDb.Description,
                    NumberOfPosts = modelDb.Posts?.Count() ?? 0,
                    NumberOfUsers = _forumService.GetAllActiveUsers(forumId: id).Count()
                };
                return View(model);
            }
        }

        [Authorize(Roles = AppConst.Role.AdminRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(ForumViewModel model)
        {
            await _forumService.Save(model: model);
            if (model.Id == 0)
                _toastNotification.AddSuccessToastMessage("The forum has been created");
            else
                _toastNotification.AddSuccessToastMessage("The forum has been updated");
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = AppConst.Role.AdminRole)]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _forumService.Delete(forumId: id);
                _toastNotification.AddSuccessToastMessage("The forum has been deleted");
                return StatusCode(200);
            }
            catch (Exception ex)
            {

                return StatusCode(400, ex.Message);
            }
        }

    }
}
