using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    [Authorize] // filter, or checking in methods manually using User.Idnetity.IsAuthenticated
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        //private readonly int userId;
        public UserController(IUserService userService)
        {
            _userService = userService;
            
        }

        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var purchasedList = await _userService.GetUserPurchasedMovies(userId);
            return View(purchasedList);
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var favoriteList = await _userService.GetUserFavoriteMovies(userId);
            return View(favoriteList);
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userProfile = await _userService.GetUserProfile(userId);
            return View(userProfile);
        }

        [HttpGet]
        public IActionResult EditProfile()
        {
            return View();
        }
    }
}
