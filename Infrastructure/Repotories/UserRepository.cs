using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repotories
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
		{
		}

		public User GetUserByEmail(string email)
		{
			return _dbContext.Users.FirstOrDefault(x => x.Email == email);
		}
	}
}
