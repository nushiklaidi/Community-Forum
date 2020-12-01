using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Model;
using CleanArchitecture.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infra.Data.Repositories
{
    public class ForumRepository : GenericRepository<Forum>, IForumRepository
    {
        public ForumRepository(AppDbContext context) : base(context) { }
    }
}
