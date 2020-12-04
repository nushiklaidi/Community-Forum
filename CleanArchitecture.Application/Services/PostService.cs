using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _uow;
        private readonly IForumService _forumService;

        public PostService(IUnitOfWork uow, IForumService forumService)
        {
            _uow = uow;
            _forumService = forumService;
        }

        public IEnumerable<Post> GetFilteredPosts(Forum model, string searchQuery)
        {
            var search = searchQuery;
            return string.IsNullOrEmpty(searchQuery) ? model.Posts :
                model.Posts.Where(p => p.Title.ToLower().Contains(search)
                || p.Content.Contains(search));
        }

        public IEnumerable<PostViewModel> GetPostsByForum(Forum model)
        {
            var posts = GetFilteredPosts(model: model, searchQuery: null).ToList();

            var postListings = posts.Select(p => new PostViewModel
            {
                Id = p.Id,
                AuthorId = p.ApplicationUser.Id.ToString(),
                AuthorRating = p.ApplicationUser.Rating,
                Title = p.Title,
                DatePosted = p.Created.ToString(),
                RepliesCount = p.PostReplies.Count(),
                AuthorName = p.ApplicationUser.UserName,
                Forum = _forumService.BuildForum(model: p)
            });
            return postListings;
        }

        public Post GetById(int postId)
        {
            var modelDb = _uow.PostRepository.Get(f => f.Id == postId).FirstOrDefault();
            if (modelDb is null)
            {
                throw new ApplicationException("Not Found");
            }
            return modelDb;
        }

        public async Task Delete(int postId)
        {
            var modelDb = GetById(postId);
            await _uow.PostRepository.Delete(modelDb);
            try
            {
                await _uow.SavechangesAsync();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Deleted failed", ex);
            }
        }
    }
}
