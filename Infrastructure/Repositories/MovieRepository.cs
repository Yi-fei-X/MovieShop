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
    public class MovieRepository : IMovieRepository
    {
        public MovieShopDbContext _dbContext;
        public MovieRepository(MovieShopDbContext dbContext)    //Why?
        {
            _dbContext = dbContext;
        }

        public async Task<Movie> GetMovieById(int id)
        {
            var movie = await _dbContext.Movie.Include(m => m.Casts).ThenInclude(m => m.Cast)
                .Include(m => m.Genres).ThenInclude(m => m.Genre).Include(m => m.Trailers)
                .FirstOrDefaultAsync(m => m.Id == id);
            // Include will first go to MovieCast table (navigation property of movie table)
            // ThenInclude will further go to cast table (navigation property of MovieCast table)
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetTop30RevenueMovies() //Make it async
        {
            // We are going to use EF with LINQ to get top 30 movies by revenue.
            // I/O bound operation
            // You can only await Tasks
            // EF and Dapper have both synchronous and asynchronous methods
            var movies = await _dbContext.Movie.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();  // ToListAsync means immediate execution async method
            return movies;
        }
    }
}
