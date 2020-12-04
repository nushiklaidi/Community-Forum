using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Domain.Interfaces;

namespace CleanArchitecture.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _uow;

        public PostService(IUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}
