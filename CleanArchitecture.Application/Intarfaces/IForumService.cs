using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Model;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Intarfaces
{
    public interface IForumService
    {
        IEnumerable<ForumListViewModel> GetAll();
        Forum GetById(int forumId);

        IEnumerable<ApplicationUser> GetAllActiveUsers(int forumId);
        bool HasRecentPost(int forumId);
    }
}
