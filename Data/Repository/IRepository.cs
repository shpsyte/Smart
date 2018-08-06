using Core.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IRepository<T> where T : class
    {


        T Add(T entity, bool save = true);
        Task<T> AddAsync(T entity, bool save = true);

        T Delete(T entity, bool save = true);
        Task<T> DeleteAsync(T entity, bool save = true);

        T Update(T entity, bool save = true);
        Task<T> UpdateAsync(T entity, bool save = true);
        

        void Save();
        Task<int> SaveAsync();
        void Dispose();



        T SingleOrDefault();
        T SingleOrDefault(Expression<Func<T, bool>> where);
        Task<T> SingleOrDefaultAsync();
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> where);


        int Count();
        int Count(Expression<Func<T, bool>> where);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> where);


        T Find(params object[] key);
        Task<T> FindAsync(params object[] key);


        IQueryable<T> FindBy(Expression<Func<T, bool>> where);
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> where);


        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where);

        IQueryable<T> Query();
        IQueryable<T> Query(Expression<Func<T, bool>> where);

      




    }
}
