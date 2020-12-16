using CleanArchitecture.Application.AppOptions;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly AppSettings _appSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(IUnitOfWork uow, IOptions<AppSettings> appSettings, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _uow = uow;
            _appSettings = appSettings.Value;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public ApplicationUser Authenticate(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task AuthenticateUser(AuthViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(email: model.Email);
            if (user == null)
                throw new ApplicationException("Your Email does not exist");
            if (user.IsActive)
            {
                var result = await _signInManager.PasswordSignInAsync(userName: model.Email, password: model.Password, isPersistent: false, lockoutOnFailure: false);
                if (!result.Succeeded)
                    throw new ApplicationException("Password is not correct");
            }
            else
            {
                throw new ApplicationException("Your account is not activated");
            }
        }
    }
}
