using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        public IUserRepository _userRepository { get; private set; }
        public IForumRepository _forumRepository { get; private set; }
        public IPostRepository _postRepository { get; private set; }
        
        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public int SaveChanges()
        {
            return _appDbContext.SaveChanges();
        }

        public Task<int> SavechangesAsync()
        {
            return _appDbContext.SaveChangesAsync();
        }

        public IUserRepository UserRepository => _userRepository = _userRepository ?? new UserRepository(_appDbContext);
        public IForumRepository ForumRepository => _forumRepository = _forumRepository ?? new ForumRepository(_appDbContext);
        public IPostRepository PostRepository => _postRepository = _postRepository ?? new PostRepository(_appDbContext);
    }
}
