using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repotories
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<User> GetUserByEmail(string email)
		{
			return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
		}

		public async Task<User> GetPurchasesById(int id)
        {
			var userPurchaseDetails = await _dbContext.Users.Include(u => u.Purchases).ThenInclude(u => u.Movie)
											.FirstOrDefaultAsync(u => u.Id == id);
			return userPurchaseDetails;
        }
		public async Task<User> GetFavoritesById(int id)
		{
			var userFavoriteDetails = await _dbContext.Users.Include(u => u.Favorites).ThenInclude(u => u.Movie)
											.FirstOrDefaultAsync(u => u.Id == id);
			return userFavoriteDetails;
		}
	}
}
