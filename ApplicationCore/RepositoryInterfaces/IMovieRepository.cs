using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository: IAsyncRepository<Movie>  //All the repository interfaces should inherit IAsyncRepository.
    {
        // method that going to get 30 highest revenue movies
        Task<IEnumerable<Movie>> GetTop30RevenueMovies();   //Make it async
        Task<Movie> GetMovieById(int id);
    }
}
