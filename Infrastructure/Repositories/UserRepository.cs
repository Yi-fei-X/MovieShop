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
    public class UserRepository : EfRepository<User>, IUserRepository   //All the repositories should inherit from EfRepository(class). We can only inherit one class but we can inherit multiple interfaces.
    {
        /*private readonly MovieShopDbContext _dbContext;
        public UserRepository(MovieShopDbContext dbContext)    //Why?
        {
            _dbContext = dbContext;
        }*/
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        /*public async Task<User> AddUser(User user)
        {
            await _dbContext.User.AddAsync(user);
            await _dbContext.SaveChangesAsync();    //only when you call SaveChanges method it will actually change the database
            return user;    //Once inserted, we will have an id and EF will automatically map that id into this user object. We return the whole object here incase we need it. We can also return bool or int. 
        }*/

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}
