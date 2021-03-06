using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.RepositoryInterfaces;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using ApplicationCore.Entities;

namespace Infrastructure.Services
{
	public class AccountService : IAccountService
	{
		private readonly IUserRepository _userRepository;
		public AccountService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}
		public async Task<int> RegisterUser(UserRegisterRequestModel model)
		{
			//check the email not exist in database
			var dbUser = await _userRepository.GetUserByEmail(model.Email);
			if (dbUser != null)
			{
				//throw new Exception("Email already exists.");
				return 0;
			}	

			//if not, continue register
			//create salt
			var salt = GenerateSalt();
			//create hashedpassword
			var hashedPassword = GenerateHashedPassword(model.Password, salt);
			//save to database
			var newUser = new User { 
				Email = model.Email,
				HashedPassword = hashedPassword,
				Salt = salt,
				DateOfBirth = model.DateOfBirth,
				FirstName = model.FirstName,
				LastName = model.LastName
			};
			//return new User id
			var createdUser = await _userRepository.Add(newUser);
			return createdUser.Id;
		}

		public async Task<UserLoginResponseModel> ValidateUser(LoginRequestModel model)
		{
			// check the hashed password is correct
			var user = await _userRepository.GetUserByEmail(model.Email);
			if (user != null)
            {
				var hashedPassword = GenerateHashedPassword(model.Password, user.Salt);
				if (hashedPassword == user.HashedPassword)
				{
					var userLoginResponseModel = new UserLoginResponseModel
					{
						Id = user.Id,
						Email = user.Email,
						DateOfBirth = user.DateOfBirth,
						FirstName = user.FirstName,
						LastName = user.LastName
					};
					return userLoginResponseModel;
				}
			}
			return null;
		}

		private string GenerateSalt()
		{
			byte[] randomBytes = new byte[128 / 8];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(randomBytes);
			}

			return Convert.ToBase64String(randomBytes);
		}

		private string GenerateHashedPassword(string password, string salt)
		{
			var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
																	password: password,
																	salt: Convert.FromBase64String(salt),
																	prf: KeyDerivationPrf.HMACSHA512,
																	iterationCount: 10000,
																	numBytesRequested: 256 / 8));
			return hashed;
		}
	}
}
