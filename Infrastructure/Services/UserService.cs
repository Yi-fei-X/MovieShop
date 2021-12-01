using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using ApplicationCore.Entities;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;   //DI
        private readonly IPurchaseRepository _purchaseRepository;
        public UserService(IUserRepository userRepository, IPurchaseRepository purchaseRepository)
        {
            _userRepository = userRepository;
            _purchaseRepository = purchaseRepository;
        }
        public async Task<int> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // Check whether email exists in the database
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser != null)
            {
                // email exists in the database
                throw new Exception("Email already exists, please login");
            }
            // generate a random unique salt
            var salt = GetSalt();
            // create the hashed password with salt generated in the above step
            var hashedPassword = GetHashedPassword(requestModel.Password, salt);
            // save the user object to db
            // create user entity object
            var user = new User
            {
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                DateOfBirth = requestModel.DateOfBirth
            };
            // use EF to save this user in the user table
            var newUser = await _userRepository.Add(user);
            return newUser.Id;

        }
        private string GetSalt()    //copy from official website
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);

        }
        private string GetHashedPassword(string password, string salt)  //copy from official website
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        public async Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel requestModel)
        {
            // get the salt and hashedpassword from databse for this user
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser == null) throw null;
            // hash the user entered password with salt from the database
            var hashedPassword = GetHashedPassword(requestModel.Password, dbUser.Salt);
            // check the hashedpassword with database hashed password
            if (hashedPassword == dbUser.HashedPassword)
            {
                // user entered correct password
                var userLoginResponseModel = new UserLoginResponseModel
                {
                    Id = dbUser.Id,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    DateOfBirth = dbUser.DateOfBirth.GetValueOrDefault(),
                    Email = dbUser.Email
                };
                return userLoginResponseModel;
            }
            return null;
        }

        public async Task<PurchaseResponseModel> GetUserPurchases(int userId)
        {
            var purchasedMovies = await _purchaseRepository.GetUserPurchases(userId);
            var movies = new PurchaseResponseModel
            {
                Id = userId,
                PurchasedMovies = new List<MovieCardResponseModel>()
            };
            foreach (var movie in purchasedMovies)
            {
                movies.PurchasedMovies.Add(new MovieCardResponseModel
                {
                    Id = movie.UserId,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl
                });
            }
            return movies;
        }
    }
}
