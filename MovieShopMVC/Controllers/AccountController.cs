using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
		public AccountController(IAccountService accountService)
		{
            _accountService = accountService;
		}

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel registerRequestModel)
        {
            //send data to service: convert into User entity, save in User datbase
            if (await _accountService.RegisterUser(registerRequestModel) == 0)
			{
                //Email already exists
                return View();
            }
            return RedirectToAction("Login");
            
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel loginRequestModel)
        {
            var user = await _accountService.ValidateUser(loginRequestModel);
            if (user == null)
            {
                //send message to enter correct email/password
            }
            // create claims that we are going to store in the cookie
            var claims = new List<Claim> { 
                new Claim(ClaimTypes.Email, user.Email),
                new Claim (ClaimTypes.GivenName, user.FirstName),
                new Claim (ClaimTypes.Surname, user.LastName),
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim (ClaimTypes.DateOfBirth, value: user.DateOfBirth.GetValueOrDefault().ToString()),
                new Claim ("Language", "English") //custom-made claim type
            };

            // Identity object that is going to store the claimns and tell it to store those inside the cookie
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // create the cookie 
            // ASP.NET(both core and old asp.net) we have one very very important class called HttpContext
            // HttpContext captures everthing about http request
            // what kind of http method GET/POST/PUT, URL, FORM, Cookies, Headers

            // create the cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            // return to home page
            return LocalRedirect("~/");
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
