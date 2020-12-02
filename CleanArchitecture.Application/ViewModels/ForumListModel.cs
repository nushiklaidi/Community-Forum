using System.Collections.Generic;

namespace CleanArchitecture.Application.ViewModels
{
    public class ForumListModel
    {
        public IEnumerable<ForumViewModel> ForumList { get; set; }
    }
}
