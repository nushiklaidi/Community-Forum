using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Application.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.MVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IToastNotification _toastNotification;

        public PostController(IToastNotification toastNotification, IPostService postService)
        {
            _toastNotification = toastNotification;
            _postService = postService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = AppConst.Role.AdminRole)]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _postService.Delete(postId: id);
                _toastNotification.AddSuccessToastMessage("The post has been deleted");
                return StatusCode(200);
            }
            catch (Exception ex)
            {

                return StatusCode(400, ex.Message);
            }
        }
    }
}
