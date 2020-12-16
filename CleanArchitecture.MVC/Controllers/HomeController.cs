using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;

namespace CleanArchitecture.MVC.Controllers
{
    public class HomeController : Controller
    {      
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult NoAccess()
        {
            ViewBag.ErrorMessage = "You have not access for this page";
            ViewBag.Contact = "Please contact with Administrator system";
            return View("_ErrorPartial");
        }

        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ErrorMessage = exceptionDetails.Error.Message;
            return View("_ErrorPartial");
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Page 404 - Sorry, the resource you requested could not be found";
                    break;
            }
            return View("_ErrorPartial");
        }
    }
}
