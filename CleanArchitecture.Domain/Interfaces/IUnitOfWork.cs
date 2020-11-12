using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        Task<int> SavechangesAsync();

        IUserRepository UserRepository { get; }
    }
}
