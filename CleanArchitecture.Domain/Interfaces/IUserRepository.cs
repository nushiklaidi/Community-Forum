using CleanArchitecture.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}
