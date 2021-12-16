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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDetailsModel> GetUserProfile(int id)
        {
            User user = await _userRepository.GetById(id);
            UserDetailsModel userProfile = new UserDetailsModel {
                Email = user.Email,
                FirstName = user.FirstName, 
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber
            };
            return userProfile;
        }
        public async Task<bool> EditUserProfile(UserDetailsModel userDetailsModel)
        {
            User user = await _userRepository.GetUserByEmail(userDetailsModel.Email);
            if (user == null) return false;

            user.FirstName = userDetailsModel.FirstName;
            user.LastName = userDetailsModel.LastName;
            user.DateOfBirth = userDetailsModel.DateOfBirth;
            user.PhoneNumber = userDetailsModel.PhoneNumber;

            if (await _userRepository.Update(user) == null)
                return false;
            return true;
        }

        public async Task<IEnumerable<MovieCardResponseModel>> GetUserFavoriteMovies(int id)
        {
            //get favorite movies
            var user = await _userRepository.GetFavoritesById(id);

            List<MovieCardResponseModel> movieFavorites = new List<MovieCardResponseModel>();
            foreach (var item in user.Favorites)
            {
                movieFavorites.Add(new MovieCardResponseModel
                {
                    Id = item.MovieId,
                    Title = item.Movie.Title,
                    PosterUrl = item.Movie.PosterUrl
                });
            }
            return movieFavorites;
        }

        public async Task<IEnumerable<MovieCardResponseModel>> GetUserPurchasedMovies(int id)
        {
            //get purchased movies
            var user = await _userRepository.GetPurchasesById(id);

            List<MovieCardResponseModel> moviePurchases = new List<MovieCardResponseModel>();
            foreach (var item in user.Purchases)
            {
                moviePurchases.Add(new MovieCardResponseModel
                {
                    Id = item.MovieId,
                    Title = item.Movie.Title,
                    PosterUrl = item.Movie.PosterUrl
                });
            }
            return moviePurchases;
        }
    }
}
