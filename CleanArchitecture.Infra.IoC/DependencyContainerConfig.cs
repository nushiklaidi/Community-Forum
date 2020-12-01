using CleanArchitecture.Application.AppOptions;
using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Application.Validation;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Infra.Data.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infra.IoC
{
    public static class DependencyContainerConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            //CleanArchitecture.Application
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IForumService, ForumService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailSender, EmailSender>();

            //CleanArchitecture.Domain.Interfaces | CleanArchitecture.Infra.Data.Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IForumRepository, ForumRepository>();

            //CleanArchitecture.Domain.Interfaces | CleanArchitecture.Infra.Data.Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Validator
            services.AddTransient<IValidator<AuthViewModel>, AuthViewModelVal>();
            services.AddTransient<IValidator<UserViewModel>, UserViewModelVal>();
        }
    }
}
