using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        private readonly IAsyncRepository<Genre> _genreRepository;
        private readonly IMemoryCache _memoryCache;

        // Caching (E.G. Genres)
        // Cache is stored in Key / Value pair. (E.G. GENRES / List<Genres>)
        public GenreService(IAsyncRepository<Genre> genreRepository, IMemoryCache memoryCache)
        {
            _genreRepository = genreRepository;
            _memoryCache = memoryCache;
        }
        public async Task<IEnumerable<GenreModel>> GetAllGenres()
        {
            // check if the cache has the genres. If yes, take genres from cache
            // If no, go to database, then get the genres and store in the cache.

            // How to do Caching:
            // 1. .NET in memory caching. (Recommend for small amount of data)
            // 2. Distributed caching (Third party library). E.G. Redis Cache. (Recommend for large amount of data)

            // GetOrCreateAsnyc: If cache exists, then directly get the data from cache, else create that cache and use the following code to get the data from database.
            var genres = await _memoryCache.GetOrCreateAsync("genresData", async entry =>   
            {
                entry.SlidingExpiration = TimeSpan.FromDays(30);    // Valid for 30 days
                return await _genreRepository.GetAll();
            });

            // var genres = await _genreRepository.GetAll();
            var genresModel = new List<GenreModel>();
            foreach (var genre in genres)
            {
                genresModel.Add(new GenreModel { Id = genre.Id, Name = genre.Name });
            }
            return genresModel;
        }
    }
}
