using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Intarfaces
{
    public interface IForumService
    {
        IEnumerable<ForumListViewModel> GetAll();
        Forum GetById(int forumId);
        Task Create(ForumAddViewModel model);

        IEnumerable<ApplicationUser> GetAllActiveUsers(int forumId);
        bool HasRecentPost(int forumId);
    }
}
