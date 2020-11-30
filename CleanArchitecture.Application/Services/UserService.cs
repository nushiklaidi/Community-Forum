using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Model;
using CleanArchitecture.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using CleanArchitecture.Application.Model;

namespace CleanArchitecture.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UserService(IUnitOfWork uow, AppDbContext db, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _uow = uow;
            _db = db;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }
        
        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            return await _uow.UserRepository.GetAll();
        }

        public async Task<UserViewModel> Get(string userId, string currentUserId)
        {
            var modelDb = await _uow.UserRepository.GetById(userId);
            if (modelDb is null)
            {
                throw new ApplicationException("Not Found");
            }
            
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();
            var role = userRole.FirstOrDefault(u => u.UserId == modelDb.Id);

            var adminRole = userRole.FirstOrDefault(u => u.UserId == currentUserId);
            var adminUserRole = roles.FirstOrDefault(u => u.Id == adminRole.RoleId);

            if (modelDb.Id != currentUserId && adminUserRole.Name != AppConst.Role.AdminRole)
            {
                throw new ApplicationException("You can't view enother user profile");
            }

            var model = new UserViewModel()
            {
                Id = modelDb.Id,
                UserName = modelDb.UserName,
                Email = modelDb.Email,
                Rating = modelDb.Rating,
                ImageUrl = modelDb.ProfileImageUrl,
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
            var webRootPath = _hostingEnvironment.WebRootPath;
            var modelDb = await _uow.UserRepository.GetById(model.Id);

            if (modelDb is null)
            {
                throw new ApplicationException("Not Found");
            }

            var userRole = _db.UserRoles.FirstOrDefault(u => u.UserId == modelDb.Id);

            //Update user role
            if (userRole.RoleId != model.RoleId)
            {
                var previousRoleName = _db.Roles.Where(u => u.Id == userRole.RoleId).Select(e => e.Name).FirstOrDefault();
                var newRoleName = _db.Roles.Where(u => u.Id == model.RoleId).Select(e => e.Name).FirstOrDefault();

                //Remove the old role
                await _userManager.RemoveFromRoleAsync(modelDb, previousRoleName);

                //Add the new role
                await _userManager.AddToRoleAsync(modelDb, newRoleName);
            }

            //Update image profile
            if (model.ProfileImageUrl != null && model.ProfileImageUrl.Length > 0)
            {
                var uploadDir = @"img";
                var fileName = Path.GetFileNameWithoutExtension(model.ProfileImageUrl.FileName);
                var extension = Path.GetExtension(model.ProfileImageUrl.FileName);
                fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;
                var path = Path.Combine(webRootPath, uploadDir, fileName);
                await model.ProfileImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                modelDb.ProfileImageUrl = "/" + uploadDir + "/" + fileName;
            }
            modelDb.UserName = model.UserName;
            try
            {
                await _uow.SavechangesAsync();
            }
            catch (Exception)
            {
                throw new ApplicationException("Update failed");
            }                      
        }

        public async Task ActivateUser(string id)
        {
            var user = await _uow.UserRepository.GetById(id);
            if (user is null)
            {
                throw new ApplicationException("Not Found");
            }
            user.IsActive = true;
            try
            {
                await _uow.SavechangesAsync();
            }
            catch (Exception)
            {
                throw new ApplicationException("Update failed");
            }           
        }

        public async Task DeactivateUser(string id)
        {
            var user = await _uow.UserRepository.GetById(id);
            if (user is null)
            {
                throw new ApplicationException("Not Found");
            }
            user.IsActive = false;
            try
            {
                await _uow.SavechangesAsync();
            }
            catch (Exception)
            {
                throw new ApplicationException("Update failed");
            }
        }        
    }
}
