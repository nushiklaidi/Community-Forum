﻿using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Model;
using CleanArchitecture.Infra.Data.Context;

namespace CleanArchitecture.Infra.Data.Repositories
{
    public class ForumRepository : GenericRepository<Forum>, IForumRepository
    {
        public ForumRepository(AppDbContext context) : base(context) { }
    }
}
