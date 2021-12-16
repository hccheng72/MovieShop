using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
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
        public GenreService(IGenreRepository genreRepository) //need DI
        {
            _genreRepository = genreRepository;
        }

        public async Task<List<GenreModel>> GetAllGenres()
        {
            List<Genre> genres = await _genreRepository.GetAll();
            List<GenreModel> genreModels = new List<GenreModel>();
            foreach (var genre in genres)
            {
                genreModels.Add(new GenreModel { 
                    Id = genre.Id,
                    Name = genre.Name
                });
            }
            return genreModels.OrderBy(g => g.Name).ToList();
        }
    }
}
