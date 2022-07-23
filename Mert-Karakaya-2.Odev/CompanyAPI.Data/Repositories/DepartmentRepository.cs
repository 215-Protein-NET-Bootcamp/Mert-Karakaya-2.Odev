using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CompanyAPI.Core;
using CompanyAPI.Data.Model;

namespace CompanyAPI.Data.Repositories
{
    public class DepartmentRepository : IBaseRepository<Department>
    {
        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Department>> Where(Expression<Func<Department, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(Department entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Department entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Department entity)
        {
            throw new NotImplementedException();
        }
    }
}
