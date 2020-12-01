using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.Application.Services
{
    public class ForumService : IForumService
    {
        private readonly IUnitOfWork _uow;

        public ForumService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<ForumListViewModel> GetAll()
        {
            var modelDb = _uow.ForumRepository.GetAll(includeProperties: "Posts").Select(f => new ForumListViewModel
            {
                Id = f.Id,
                Title = f.Title,
                Description = f.Description,
                NumberOfPosts = f.Posts?.Count() ?? 0,
                NumberOfUsers = GetAllActiveUsers(f.Id).Count(),
                ForumImageUrl = f.ImageUrl,
                HasRecentPost = HasRecentPost(f.Id)
            }).OrderBy(f => f.Title);
            return modelDb;
        }

        public Forum GetById(int forumId)
        {
            var modelDb = _uow.ForumRepository.Get(f => f.Id == forumId, "Posts.ApplicationUser,Posts.PostReplies.ApplicationUser").FirstOrDefault();
            if (modelDb is null)
            {
                throw new ApplicationException("Not Found");
            }
            return modelDb;
        }

        public IEnumerable<ApplicationUser> GetAllActiveUsers(int forumId)
        {
            var modelDb = GetById(forumId).Posts;
            if (modelDb != null || !modelDb.Any())
            {
                var postUsers = modelDb.Select(p => p.ApplicationUser);
                var replyUsers = modelDb.SelectMany(p => p.PostReplies).Select(r => r.ApplicationUser);
                return postUsers.Union(replyUsers).Distinct();
            }
            else
            {
                throw new ApplicationException("Not Found");
            }
        }

        public bool HasRecentPost(int forumId)
        {
            const int hoursAgo = 24;
            var window = DateTime.Now.AddHours(-hoursAgo);
            var modelDb = GetById(forumId).Posts.Any(p => p.Created > window);
            return modelDb;
        }
    }
}
