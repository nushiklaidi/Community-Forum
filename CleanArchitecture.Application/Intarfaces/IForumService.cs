using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Intarfaces
{
    public interface IForumService
    {
        IEnumerable<ForumViewModel> GetAll();
        Forum GetById(int forumId);
        Task Save(ForumViewModel model);
        Task Delete(int forumId);
        ForumViewModel BuildForum(Post model);
        ForumViewModel BuildForum(Forum model);

        IEnumerable<ApplicationUser> GetAllActiveUsers(int forumId);
        bool HasRecentPost(int forumId);
    }
}
