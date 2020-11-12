using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Application.Validation;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Infra.Data.Repositories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infra.IoC
{
    public static class DependencyContainerConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            //CleanArchitecture.Application
            services.AddScoped<IAuthService, AuthService>();

            //CleanArchitecture.Domain.Interfaces | CleanArchitecture.Infra.Data.Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();

            //CleanArchitecture.Domain.Interfaces | CleanArchitecture.Infra.Data.Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Validator
            services.AddTransient<IValidator<AuthViewModel>, AuthViewModelVal>();
        }
    }
}
