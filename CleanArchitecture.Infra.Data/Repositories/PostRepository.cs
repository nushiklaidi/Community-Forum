using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Model;
using CleanArchitecture.Infra.Data.Context;

namespace CleanArchitecture.Infra.Data.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context) { }
    }
}
