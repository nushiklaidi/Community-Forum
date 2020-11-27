using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Model;
using CleanArchitecture.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IUnitOfWork uow, AppDbContext db, UserManager<ApplicationUser> userManager)
        {
            _uow = uow;
            _db = db;
            _userManager = userManager;
        }
               
        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            return await _uow.UserRepository.GetAll();
        }

        public async Task<UserViewModel> Get(string id)
        {
            var modelDb = await _uow.UserRepository.GetById(id);
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();
            var role = userRole.FirstOrDefault(u => u.UserId == modelDb.Id);
            var model = new UserViewModel()
            {
                Id = modelDb.Id,
                UserName = modelDb.UserName,
                Email = modelDb.Email,
                Rating = modelDb.Rating,
                ProfileImageUrl = modelDb.ProfileImageUrl,
                MemberSince = modelDb.MemberSince,
                IsActive = modelDb.IsActive,
                RoleId = roles.FirstOrDefault(u => u.Id == role.RoleId).Id,
                RoleList = _db.Roles.Select(u => new SelectListItem
                { 
                    Text = u.Name,
                    Value = u.Id
                })
            };
            return model;
        }

        public async Task Update(UserViewModel model)
        {
            var modelDb = await _uow.UserRepository.GetById(model.Id);
            var userRole = _db.UserRoles.FirstOrDefault(u => u.UserId == modelDb.Id);

            if (userRole.RoleId != model.RoleId)
            {
                var previousRoleName = _db.Roles.Where(u => u.Id == userRole.RoleId).Select(e => e.Name).FirstOrDefault();
                var newRoleName = _db.Roles.Where(u => u.Id == model.RoleId).Select(e => e.Name).FirstOrDefault();

                //Remove the old role
                await _userManager.RemoveFromRoleAsync(modelDb, previousRoleName);

                //Add the new role
                await _userManager.AddToRoleAsync(modelDb, newRoleName);
            }

            modelDb.UserName = model.UserName;
            await _uow.SavechangesAsync();            
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
