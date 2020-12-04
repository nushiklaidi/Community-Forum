using System.Collections.Generic;

namespace CleanArchitecture.Application.ViewModels
{
    public class ForumDetailsViewModel
    {
        public ForumViewModel Forum { get; set; }
        public IEnumerable<PostViewModel> Posts { get; set; }
        public string SearchQuery { get; set; }
    }
}
