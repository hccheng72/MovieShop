using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;
using System.Diagnostics;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        public HomeController(IMovieService movieService, IGenreService genreService) //DI?
        {
            _movieService = movieService;
            _genreService = genreService;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
			// _movieService = new MovieMockService(new MovieRepository());
			// Call Movie Service to get list of movie cards to show in the index view
			// 3 ways to pass the data/models from Controller Action methods to Views
			// * 1. Pass the Models in the View Method 
			//   2. ViewBag
			//   3. ViewData
            
            // I/O bound operation
            // CPU bound operation
			var movieCards = await _movieService.GetHighestGrossingMovies();
			return View(movieCards);
		}

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TopMovies()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}