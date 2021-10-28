using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieService: IMovieService
    {
        public List<MovieCardResponseModel> GetTop30RevenueMovies()
        {
            var movieCards = new List<MovieCardResponseModel> { };
            return movieCards;
        }
    }
}
