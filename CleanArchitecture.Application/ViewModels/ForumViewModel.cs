using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.ViewModels
{
    public class ForumViewModel
    {
        public IEnumerable<ForumListViewModel> ForumList { get; set; }
    }
}
