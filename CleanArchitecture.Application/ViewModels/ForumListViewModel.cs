using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.ViewModels
{
    public class ForumListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ForumImageUrl { get; set; }

        public int NumberOfPosts { get; set; }
        public int NumberOfUsers { get; set; }
        public bool HasRecentPost { get; set; }
    }
}
