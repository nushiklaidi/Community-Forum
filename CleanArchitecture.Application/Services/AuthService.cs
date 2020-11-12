using CleanArchitecture.Application.AppOptions;
using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly AppSettings _appSettings;

        public AuthService(IUnitOfWork uow, IOptions<AppSettings> appSettings)
        {
            _uow = uow;
            _appSettings = appSettings.Value;
        }

        public ApplicationUser Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
