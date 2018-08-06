using Core.Domain.Base;
using Core.Domain.PersonAndData;
using Data.Repository;
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
        private int _businessEntityId;
        private readonly ILogger _logger;


        #region ctor_methods

        public Services(SmartContext context, IRepository<T> repository, IUser currentUser, ILogger<Services<T>> logger)
        {
            this._context = context;
            this._currentUser = currentUser;
            this._logger = logger;
            this._businessEntityId = _currentUser.BusinessEntityId();
            this._repository = repository;

        }


        private T CheckTOnBusinessEntity(T entity)
        {
            if (entity != null)
            {
                if (entity.BusinessEntityId != _businessEntityId)
                {
                    entity = null;
                }
            }

            return entity;
        }
        private T InjectBusinessEntity(T entity)
        {
            entity.BusinessEntityId = _businessEntityId;
            return entity;
        }

        private Expression<Func<T, bool>> InjectBusinessEntity(Expression<Func<T, bool>> and = null)
        {
            Expression<Func<T, bool>> expr1 = p => (p.BusinessEntityId == _businessEntityId);

            if (and != null)
            {
                Expression<Func<T, bool>> expr2 = and;

                var body = Expression.AndAlso(expr1.Body, expr2.Body);
                var lambda = Expression.Lambda<Func<T, bool>>(body, expr1.Parameters[0]);

                return lambda;
            }
            else
                return expr1;

        }
        #endregion


        public virtual T Add(T entity, bool save = true) => _repository.Add(InjectBusinessEntity(entity), save);
        public virtual async Task<T> AddAsync(T entity, bool save = true) => await _repository.AddAsync(InjectBusinessEntity(entity), save);
        public virtual T Delete(T entity, bool save = true) => _repository.Delete(InjectBusinessEntity(entity), save);
        public virtual async Task<T> DeleteAsync(T entity, bool save = true) => await _repository.DeleteAsync(InjectBusinessEntity(entity), save);
        public virtual T Update(T entity, bool save = true) => _repository.Update(InjectBusinessEntity(entity), save);
        public virtual async Task<T> UpdateAsync(T entity, bool save = true) => await _repository.UpdateAsync(InjectBusinessEntity(entity), save);


        
        public virtual void Save() => _repository.Save();
        public virtual async Task<int> SaveAsync() => await _repository.SaveAsync();
        public virtual void Dispose() => _repository.Dispose();


        public virtual T SingleOrDefault() => _repository.SingleOrDefault(InjectBusinessEntity());
        public virtual T SingleOrDefault(Expression<Func<T, bool>> where) => _repository.Query().Where(InjectBusinessEntity()).SingleOrDefault(where);

        public virtual async Task<T> SingleOrDefaultAsync() => await _repository.SingleOrDefaultAsync(InjectBusinessEntity());
        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> where) => CheckTOnBusinessEntity(await _repository.SingleOrDefaultAsync(where));
        public virtual int Count() => _repository.Count(InjectBusinessEntity());
        public virtual int Count(Expression<Func<T, bool>> where) => _repository.Count(InjectBusinessEntity(where));
        public virtual async Task<int> CountAsync() => await _repository.CountAsync(InjectBusinessEntity());
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> where) => await _repository.CountAsync(InjectBusinessEntity(where));



        public virtual T Find(params object[] key) => CheckTOnBusinessEntity(_repository.Find(key));
        public virtual async Task<T> FindAsync(params object[] key) => CheckTOnBusinessEntity(await _repository.FindAsync(key));
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> where) => _repository.FindBy(InjectBusinessEntity(where));
        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> where) => await _repository.FindByAsync(InjectBusinessEntity(where));
        
        public virtual IEnumerable<T> GetAll() => _repository.GetAll(InjectBusinessEntity());
        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> where) => _repository.GetAll(InjectBusinessEntity(where));
        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _repository.GetAllAsync(InjectBusinessEntity());
        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where) => await _repository.GetAllAsync(InjectBusinessEntity(where));
        public virtual IQueryable<T> Query() => _repository.Query(InjectBusinessEntity());
        public virtual IQueryable<T> Query(Expression<Func<T, bool>> where) => _repository.Query(InjectBusinessEntity()).Where(where);

        public virtual async Task<IQueryable<T>> QueryAsync() => _repository.Query(InjectBusinessEntity());
        public virtual async Task<IQueryable<T>> QueryAsync(Expression<Func<T, bool>> where) =>  _repository.Query(InjectBusinessEntity(where));

    }
}
