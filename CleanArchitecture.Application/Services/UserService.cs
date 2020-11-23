using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }
               
        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            return await _uow.UserRepository.GetAll();
        }

        public async Task ActivateUser(string id)
        {
            if (id != null)
            {
                var user = await _uow.UserRepository.GetById(id);
                if(user != null)
                {
                    user.IsActive = true;
                    await _uow.SavechangesAsync();
                }
            }
                
        }

        public async Task DeactivateUser(string id)
        {
            if (id != null)
            {
                var user = await _uow.UserRepository.GetById(id);
                if (user != null)
                {
                    user.IsActive = false;
                    await _uow.SavechangesAsync();
                }
            }

        }
    }
}
