using Core.Domain.Base;
using Core.Interfaces;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SmartContext _context;
        private DbSet<T> _entity;



        public Repository(SmartContext context)
        {
            this._context = context;
            _entity = context.Set<T>();

        }

        public virtual T Add(T entity)
        {
            _entity.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            _entity.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> AddAsyncNoSave(T entity)
        {
            _entity.Add(entity);
            return entity;
        }

        public virtual int Count()
        {
            return _entity.Count();
        }

        public int Count(Expression<Func<T, bool>> where)
        {
            return _entity.Where(where).Count();
        }

        public virtual async Task<int> CountAsync()
        {
            return await _entity.CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> where)
        {
            return await _entity.Where(where).CountAsync();
        }

        public virtual void Delete(T entity)
        {
            _entity.Remove(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            _entity.Where(where).ToList().ForEach(del => _entity.Remove(del));
            _context.SaveChanges();
        }

        public virtual async Task<int> DeleteAsync(T entity)
        {
            _entity.Remove(entity);
            return await _context.SaveChangesAsync();
        }
        public virtual void DeleteNoSave(T entity)
        {
           _entity.Remove(entity);
           
        }
        public virtual T Find(params object[] key)
        {
            return _entity.Find(key);
        }

        public virtual T SingleOrDefault()
        {
            return _entity.SingleOrDefault();
        }

        public virtual async Task<T> SingleOrDefaultAsync()
        {
            return await _entity.SingleOrDefaultAsync();
        }

        public virtual T SingleOrDefault(Expression<Func<T, bool>> where)
        {
            return _entity.AsNoTracking().Where(where).SingleOrDefault();
        }

        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> where)
        {
            return await _entity.AsNoTracking().Where(where).SingleOrDefaultAsync();
        }


        public virtual ICollection<T> FindAll(Expression<Func<T, bool>> where)
        {
            return _entity.Where(where).ToList();
        }

        public virtual async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> where)
        {
            return await _entity.Where(where).ToListAsync();
        }

        public virtual async Task<T> FindAsync(params object[] key)
        {
            return await _entity.FindAsync(key);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> where)
        {
            IQueryable<T> query = _entity.Where(where);
            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> where)
        {
            return await _entity.Where(where).ToListAsync();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _entity.ToList();
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> where)
        {
            return _entity.Where(where).ToList();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where)
        {
            return await _entity.Where(where).ToListAsync();
        }

        public virtual IQueryable<T> Query()
        {
            return _entity.AsNoTracking().AsQueryable();
        }

        public virtual IQueryable<T> Query(Expression<Func<T, bool>> where)
        {
            return _entity.AsNoTracking().Where(where).AsQueryable();
        }

        public virtual async Task<IQueryable<T>> QueryAsync()
        {
            var result = _entity.AsQueryable();

            //return await result.AsQueryable();
            return await Task.Run(() =>
            {
                return result.AsQueryable();
            });
        }

        public virtual async Task<IQueryable<T>> QueryAsync(Expression<Func<T, bool>> where)
        {
            var result = _entity.Where(where).AsQueryable();
            return await Task.Run(() =>
            {
                return result.AsQueryable();

            });
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }

        public virtual async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public virtual T Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }


        public virtual async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsyncNoSave(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return await Task.Run(() =>
            {
                return entity;
            });
        }
    }
}
