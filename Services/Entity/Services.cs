using Core.Domain.Base;
using Core.Domain.PersonAndData;
using Core.Interfaces;
using Data.Context;
using Data.Repository;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Entity
{
    public class Services<T> : IServices<T> where T : BaseEntity
    {
        private SmartContext _context;
        private IRepository<T> _repository;
        private IUser _currentUser;
        private string _currentUserId;
        private string _currentUserEmail;
        private int _businessEntityId;
        private readonly ILogger _logger;

        public Services(SmartContext context, IRepository<T> repository, IUser currentUser, ILogger<Services<T>> logger)
        {
            this._context = context;
            this._currentUser = currentUser;
            this._currentUserId = _currentUser.Id();
            this._currentUserEmail = _currentUser.Email();
            this._logger = logger;
            this._businessEntityId = _currentUser.BusinessEntityId();
            this._repository = repository;

        }

        T inject(T entity)
        {
            entity.BusinessEntityId = _businessEntityId;
            return entity;
        }

        public Expression<Func<T, bool>> InjectCurrentBusiness()
        {
            return p => (p.BusinessEntityId == _businessEntityId);
        }

        private List<T> ListOfEntity()
        {
            return _repository.GetAll(InjectCurrentBusiness()).ToList();
        }

        private List<T> ListOfEntity(Expression<Func<T, bool>> where)
        {
            return _repository.Query(InjectCurrentBusiness()).Where(where).ToList();
        }


        public T Add(T entity)
        {
            entity = inject(entity);
            return _repository.Add(entity);
        }

        public async Task<T> AddAsync(T entity)
        {
            entity = inject(entity);
            return await _repository.AddAsync(entity);
        }

        public async Task<T> AddAsyncNoSave(T entity)
        {
            entity = inject(entity);
            return await _repository.AddAsyncNoSave(entity);
        }

        public int Count()
        {
            return _repository.Count(InjectCurrentBusiness());
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync(InjectCurrentBusiness());
        }



        public void Delete(T entity)
        {
            entity = inject(entity);
            _repository.Delete(entity);
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            List<T> _del = ListOfEntity(where);
            _del.ForEach(a => _repository.Delete(a));
        }


        public void DeleteNoSave (T entity)
        {
            entity = inject(entity);
            _repository.DeleteAsync(entity);
        }


        public async Task<int> DeleteAsync(T entity)
        {
            entity = inject(entity);
            return await _repository.DeleteAsync(entity);
        }

        public T Find(params object[] key)
        {
            var data = _repository.Find(key);
            if (data != null)
            {
                if (data.BusinessEntityId != _businessEntityId)
                {
                    data = null;
                }
            }
            return data;
        }
        

        public async Task<T> FindAsync(params object[] key)
        {
            var data = _repository.FindAsync(key);
            if (data != null)
            {
                if (data.Result.BusinessEntityId != _businessEntityId)
                {
                    data = null;
                }
            }
            return await data;
        }
        

        public T SingleOrDefault()
        {
            var data = _repository.SingleOrDefault(a=>a.BusinessEntityId == _businessEntityId);
            return data;
        }
        
        public async Task<T> SingleOrDefaultAsync()
        {
            return await _repository.SingleOrDefaultAsync(a => a.BusinessEntityId == _businessEntityId);
        }

         public T SingleOrDefault(Expression<Func<T, bool>> where)
        {
            return _repository.Query().Where(a => a.BusinessEntityId == _businessEntityId).SingleOrDefault(where);
        }
        
        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> where)
        {
            var data = await _repository.SingleOrDefaultAsync(where);
             if (data != null)
            {
                if (data.BusinessEntityId != _businessEntityId)
                {
                    data = null;
                }
            }
            return data;
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll(InjectCurrentBusiness());
        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> where)
        {
            return _repository.GetAll(where).Where(a => a.BusinessEntityId == _businessEntityId);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync(InjectCurrentBusiness());
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where)
        {
            return await Task.Run(() =>
            {
                return _repository.GetAllAsync(where).Result.Where(a => a.BusinessEntityId == _businessEntityId);
            });
        }

      

        public IQueryable<T> Query()
        {
            return _repository.Query(InjectCurrentBusiness());
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> where)
        {
            return _repository.Query(InjectCurrentBusiness()).Where(where);
        }

        public async Task<IQueryable<T>> QueryAsync()
        {
            return await _repository.QueryAsync(InjectCurrentBusiness());
        }

        public async Task<IQueryable<T>> QueryAsync(Expression<Func<T, bool>> where)
        {
            return await Task.Run(() =>
            {
                return _repository.QueryAsync(InjectCurrentBusiness()).Result.Where(where);
            });
        }

        public void Save()
        {
            _repository.Save();
        }

        public async Task<int> SaveAsync()
        {
           return await _repository.SaveAsync();
        }

        public T Update(T entity)
        {
            entity = inject(entity);
            return _repository.Update(entity);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            entity = inject(entity);
            return await _repository.UpdateAsync(entity);
        }

        public async Task<T> UpdateAsyncNoSave(T entity)
        {
            entity = inject(entity);
            return await _repository.UpdateAsyncNoSave(entity);
        }
    }
}
