using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Properties

        private readonly IAuthService _authService;

        #endregion

        #region Constructor

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Authanticate User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Authenticate(AuthViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _authService.Authenticate(model.Username, model.Password);
                if (user == null)
                    return BadRequest(new { message = "Username or Password is incorect" });
                return Ok(user);
            }
            return BadRequest(ModelState);
        }

        #endregion

    }
}
