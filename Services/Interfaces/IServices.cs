using Core.Domain.Base;
using Core.Domain.PersonAndData;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IServices<T>
    {
        T Add(T entity);
        Task<T> AddAsync(T entity);
        Task<T> AddAsyncNoSave(T entity);

        int Count();
        Task<int> CountAsync();
        
        void Delete(Expression<Func<T, bool>> where);
        void Delete(T entity);
        Task<int> DeleteAsync(T entity);
        void DeleteNoSave(T entity);
        
        bool Equals(object obj);
        T Find(params object[] key);
        Task<T> FindAsync(params object[] key);

         T SingleOrDefault();
        Task<T> SingleOrDefaultAsync();

        T SingleOrDefault(Expression<Func<T, bool>> where);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> where);


        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where);
        IQueryable<T> Query();
        IQueryable<T> Query(Expression<Func<T, bool>> where);
        Task<IQueryable<T>> QueryAsync();
        Task<IQueryable<T>> QueryAsync(Expression<Func<T, bool>> where);
        void Save();
        Task<int> SaveAsync();
        T Update(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> UpdateAsyncNoSave(T entity);
        
    }
}
