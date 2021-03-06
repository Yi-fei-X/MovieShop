using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IPurchaseRepository: IAsyncRepository<Purchase>   //All interface repository should inherit from the base repository
    {
        Task<IEnumerable<Purchase>> GetUserPurchases(int id);
    }
}
