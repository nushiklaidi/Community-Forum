using CleanArchitecture.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.ViewModels
{
    public class UserListViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
