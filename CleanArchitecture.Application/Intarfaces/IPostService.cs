using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Intarfaces
{
    public interface IPostService
    {
        IEnumerable<Post> GetFilteredPosts(Forum model, string searchQuery);
        IEnumerable<PostViewModel> GetPostsByForum(Forum model);
        Post GetById(int postId);
        Task Delete(int postId);
    }
}
