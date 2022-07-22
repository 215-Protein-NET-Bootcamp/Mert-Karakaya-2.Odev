using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CompanyAPI.Core.Entities;

namespace CompanyAPI.Core
{
    public interface IBaseService<T> where T : class
    {
        Task<ResponseEntity> GetAllAsync();
        Task<ResponseEntity> GetByIdAsync(int id);
        Task<ResponseEntity> Where(Expression<Func<T, bool>> expression);
        Task<ResponseEntity> InsertAsync(T entity);
        Task<ResponseEntity> Update(T entity);
        Task<ResponseEntity> DeleteAsync(T entity);
    }
}