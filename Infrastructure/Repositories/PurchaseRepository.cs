using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PurchaseRepository : EfRepository<Purchase>, IPurchaseRepository   //All the repositories should inherit from EfRepository(class). We can only inherit one class but we can inherit multiple interfaces.
    {
        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Purchase>> GetUserPurchases(int userId)
        {
            var purchases = await _dbContext.Purchase.Include(m => m.Movie).Where(p => p.UserId == userId).OrderByDescending(p => p.PurchaseDateTime).ToListAsync();
            return purchases;
        }
    }
}
