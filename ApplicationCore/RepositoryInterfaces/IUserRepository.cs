using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IUserRepository: IAsyncRepository<User>    //All the repository interfaces should inherit IAsyncRepository.
    {
        Task<User> GetUserByEmail(string email);
    }
}
