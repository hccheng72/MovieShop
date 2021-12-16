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
        private readonly IGenreRepository _genreRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly string _genresCacheKey = "genres";
        private readonly TimeSpan defaultCacheDuration = TimeSpan.FromDays(7);
        public GenreService(IGenreRepository genreRepository, IMemoryCache memoryCache) //need DI
        {
            _genreRepository = genreRepository;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<GenreModel>> GetAllGenres()
        {

            IEnumerable<GenreModel> genresFromCache = await _memoryCache.GetOrCreateAsync(_genresCacheKey, CacheFactory);
            return genresFromCache;
            
        }
        private async Task<IEnumerable<GenreModel>> CacheFactory(ICacheEntry cacheEntry)
        {
            cacheEntry.SlidingExpiration = defaultCacheDuration;
            List<Genre> genres = await _genreRepository.GetAll();
            List<GenreModel> genreModels = new List<GenreModel>();
            foreach (var genre in genres)
            {
                genreModels.Add(new GenreModel
                {
                    Id = genre.Id,
                    Name = genre.Name
                });
            }
            return genreModels.OrderBy(g => g.Name).ToList();
        }
    }
}
