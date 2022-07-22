using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CompanyAPI.Core;
using CompanyAPI.Core.Entities;

namespace CompanyAPI.Service.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IUnitofWork _unitofWork;
        public BaseService(IBaseRepository<T> repository, IUnitofWork unitofWork)
        {
            _repository = repository;
            _unitofWork = unitofWork;   
        }
        public virtual async Task<ResponseEntity> GetAllAsync()
        {
            var allRecord = await _repository.GetAllAsync();
            return new ResponseEntity(allRecord);
        }

        public virtual async Task<ResponseEntity> GetByIdAsync(int id)
        {
            var result = _repository.GetByIdAsync(id);
            return new ResponseEntity(result);
        }

        public virtual async Task<ResponseEntity> Where(Expression<Func<T, bool>> expression)
        {
            var result = _repository.Where(expression);
            return new ResponseEntity(result);
        }

        public virtual async Task<ResponseEntity> InsertAsync(T entity)
        {
            try
            {
                var result = _repository.InsertAsync(entity);
                await _unitofWork.CommitAsync();
                return new ResponseEntity(result);
            }
            catch (Exception e)
            {
                return new ResponseEntity("Save Error");
            }
        }

        public virtual async Task<ResponseEntity> UpdateAsync(int id, T entity)
        {
            try
            {
                var unUpdatedEntity = await _repository.GetByIdAsync(id);
                if (unUpdatedEntity == null)
                {
                    return new ResponseEntity("No Data");
                }
                _repository.Update(entity);
                await _unitofWork.CommitAsync();
                return new ResponseEntity(entity);
            }
            catch (Exception e)
            {
                return new ResponseEntity("Update Error");
            }
        }

        public virtual async Task<ResponseEntity> DeleteAsync(int id)
        {
            try
            {
                var deleteEntity = await _repository.GetByIdAsync(id);
                if (deleteEntity == null)
                {
                    return new ResponseEntity("No Data");
                }
                _repository.Delete(deleteEntity);
                _unitofWork.Commit();
                return new ResponseEntity(deleteEntity);
            }
            catch (Exception e)
            {
                return new ResponseEntity("Delete Error");
            }
        }
    }
}
