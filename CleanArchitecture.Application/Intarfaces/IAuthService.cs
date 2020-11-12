using CleanArchitecture.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Intarfaces
{
    public interface IAuthService
    {
        User Authenticate(string username, string password);
    }
}
