﻿using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
	public interface IUserRepository: IRepository<User>
	{
		Task<User> GetUserByEmail(string email);
		Task<User> GetPurchasesById(int id);
		Task<User> GetFavoritesById(int id);
	}
}
