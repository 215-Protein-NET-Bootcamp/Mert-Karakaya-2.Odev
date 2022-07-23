using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CompanyAPI.Core;
using CompanyAPI.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Data.Repositories
{
    public class EFBaseRepository<T> : IBaseRepository<T> where T : class //T'nin ne olduğu her zaman belirtilmeli
    {
        protected readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _dbSet;
        public EFBaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = appDbContext.Set<T>(); //Context'ten set edilir.
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
