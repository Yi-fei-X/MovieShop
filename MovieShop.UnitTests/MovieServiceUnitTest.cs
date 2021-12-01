using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using ApplicationCore.Models;

namespace MovieShop.UnitTests
{
    [TestClass]     //attribute
    public class MovieServiceUnitTest   //test all the methods inside MovieService class
    {
       
        private MovieService _sut;
        private static List<Movie> _movies;
        private Mock<IMovieRepository> _mockMovieRepository;    //During run time, the mock library will create a mock repository of that interface.

        // why TestInitialize here?
        [TestInitialize]    // use [OneTimeSetup] in NUnit
        public void OneTimeSetup()  //create mock object using Moq library 
        {
            _mockMovieRepository = new Mock<IMovieRepository>();
           
            // System under test (SUT). In this case our system is MovieService.
            _sut = new MovieService(_mockMovieRepository.Object);
            _mockMovieRepository.Setup(m => m.GetTop30RevenueMovies()).ReturnsAsync(_movies);   //set up the method for the repository. (For mock repository, when someone call this GetTop30RevenueMovies method, return _movies from OneTimeSetup)
        }

        // SetUp method needs to be static
        [ClassInitialize]   //why ClassInitialize here?
        public static void SetUp(TestContext context)   //setup fake data so it can be reused in multiple testing methods 
        {
            _movies = new List<Movie>
            {
                new Movie {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
                new Movie {Id = 2, Title = "Avatar", Budget = 1200000},
                new Movie {Id = 3, Title = "Titanic", Budget = 1200000}
            };

        }

        [TestMethod]
        public async Task TestListOfHighestGrossingMoviesFromFakeData() //Method name should be very descriptive. (English sentence)
        {
            // AAA (Arrange, Act and Assert)

            // Arrange
            // Create mock objects, data, methods etc
            //_sut = new MovieService(new MockMovieRepository());     //Create mock repository manually, which is inconvenience. Use Moq library instead.


            // Act
            // call GetTop30RevenueMovies method in MovieService
            var movies = await _sut.GetTop30RevenueMovies();       // the method we test is async method

            // Assert
            // check the actual output with expected data.
            // we can do multiple assert, but don't do to many in one method. Because if one is fail, the whole test will fail.
            Assert.IsNotNull(movies);
            Assert.IsInstanceOfType(movies, typeof(IEnumerable<MovieCardResponseModel>));
            Assert.AreEqual(3, movies.Count);
        }
    }


    
}
