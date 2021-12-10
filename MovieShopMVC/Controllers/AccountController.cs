using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Login(LoginRequestModel loginRequestModel)
        {
            var user = _accountService.ValidateUser(loginRequestModel);
            if (user == null)
            {
                //send message to enter correct email/password
            }
            return View();
        }
    }
}
