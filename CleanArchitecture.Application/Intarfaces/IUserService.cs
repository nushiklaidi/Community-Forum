using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Intarfaces
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAll();
        Task<UserViewModel> Get(string id);
        Task ActivateUser(string id);
        Task DeactivateUser(string id);
        Task Update(UserViewModel model);
    }
}
