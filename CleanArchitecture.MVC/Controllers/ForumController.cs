using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.MVC.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        private readonly IForumService _forumService;

        public ForumController(IForumService forumService)
        {
            _forumService = forumService;
        }

        public IActionResult Index()

        {
            var model = new ForumViewModel
            {
                ForumList = _forumService.GetAll()
            };
            return View(model);
        }
    }
}
