﻿using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository) //DI?
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<MovieCardResponseModel>> GetHighestGrossingMovies()
        {
            // call my MovieRepository and get the data
            var movies = await _movieRepository.Get30HighestGrossingMovies();
            // 3rd party Automapper from Nuget
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(
                    new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title }
                    );
            }

            return movieCards;
        }

        public async Task<MovieDetailsResponseModel> GetMovieDetailsById(int id)
        {
            var movie = await _movieRepository.GetById(id);  //it's a Task<Movie>
            // map movie entity into movie details model
            //automapper can be used
            var movieDetails = new MovieDetailsResponseModel
            {
                Id = movie.Id,
                PosterUrl = movie.PosterUrl,
                Title = movie.Title,
                OriginalLanguage = movie.OriginalLanguage,
                Overview = movie.Overview,
                Rating = movie.Rating,
                Tagline = movie.Tagline,
                RunTime = movie.RunTime,
                BackdropUrl = movie.BackdropUrl,
                TmdbUrl = movie.TmdbUrl,
                ImdbUrl = movie.ImdbUrl,
                Price = movie.Price
            };

            foreach (var movieCast in movie.CastsOfMovie)
            {
                movieDetails.Casts.Add(new CastResponseModel
                {
                    Id = movieCast.CastId,
                    Character = movieCast.Character,
                    Name = movieCast.Cast.Name,
                    PostUrl = movieCast.Cast.ProfilePath
                });
            }

            foreach (var trailer in movie.Trailers)
            {
                movieDetails.Trailers.Add(new TrailerResponseModel
                {
                    Id = trailer.Id,
                    MovieId = trailer.MovieId,
                    TrailerUrl = trailer.TrailerUrl,
                    Name = trailer.Name
                });
            }

            foreach (var movieGenre in movie.GenresOfMovie)
            {
                movieDetails.Genres.Add(new GenreModel
                {
                    Id = movieGenre.GenreId,
                    Name = movieGenre.Genre.Name
                });
            }

            return movieDetails;
        }
    }
}
