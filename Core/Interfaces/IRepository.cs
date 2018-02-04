using Core.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : class
    {


        T Add(T entity);
        Task<T> AddAsync(T entity);
        Task<T> AddAsyncNoSave(T entity);



        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        Task<int> DeleteAsync(T entity);

        void DeleteNoSave(T entity);
        void Save();
        Task<int> SaveAsync();
        T Update(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> UpdateAsyncNoSave(T entity);

        T SingleOrDefault();
        Task<T> SingleOrDefaultAsync();

        T SingleOrDefault(Expression<Func<T, bool>> where);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> where);
        int Count();
        Task<int> CountAsync();

        int Count(Expression<Func<T, bool>> where);
        Task<int> CountAsync(Expression<Func<T, bool>> where);

        T Find(params object[] key);
        Task<T> FindAsync(params object[] key);
        ICollection<T> FindAll(Expression<Func<T, bool>> where);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> where);
        IQueryable<T> FindBy(Expression<Func<T, bool>> where);
        Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> where);


        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where);

        IQueryable<T> Query();
        IQueryable<T> Query(Expression<Func<T, bool>> where);

        Task<IQueryable<T>> QueryAsync();
        Task<IQueryable<T>> QueryAsync(Expression<Func<T, bool>> where);




    }
}
