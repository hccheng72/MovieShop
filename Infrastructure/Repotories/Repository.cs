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
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly MovieShopDbContext _dbContext;
        public Repository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T> Add(T entity)
        {
			_dbContext.Set<T>().Add(entity); //save in memory
            await _dbContext.SaveChangesAsync(); // save out memory
			return entity;
        }
        public async Task<T> Delete(int id)
        {
            //var d = _dbContext.Set<T>().Find(id);
            //if (d != null)
            //{
            //    _dbContext.Set<T>().Remove(d);
            //    _dbContext.SaveChanges();
            //}
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
