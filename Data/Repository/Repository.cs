using Core.Domain.Base;
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
        private bool disposed = false;



        public Repository(SmartContext context)
        {
            this._context = context;
            _entity = context.Set<T>();

        }

        public virtual T Add(T entity, bool save = true) 
        {
            _entity.Add(entity);

            if (save) 
              _context.SaveChanges();

            return entity;
        }

        public virtual async Task<T> AddAsync(T entity, bool save = true)
        {
            _entity.Add(entity);

            if (save)
            await _context.SaveChangesAsync();

            return entity;
        }



        public virtual T Delete(T entity, bool save = true)
        {
            _entity.Remove(entity);
            if (save)
                _context.SaveChanges();
            return entity;
        }

        public virtual async Task<T> DeleteAsync(T entity, bool save = true)
        {
            _entity.Remove(entity);
            if (save)
                await _context.SaveChangesAsync();
            return entity;
        }

        public virtual T Update(T entity, bool save = true)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity, bool save = true)
        {
            _context.Entry(entity).State = EntityState.Modified;
            if (save)
                await _context.SaveChangesAsync();
            return entity;
        }


        public virtual int Count() => _entity.Count();
        public int Count(Expression<Func<T, bool>> where) => _entity.Where(where).Count();
        public virtual async Task<int> CountAsync() => await _entity.CountAsync();
        public async Task<int> CountAsync(Expression<Func<T, bool>> where) => await _entity.Where(where).CountAsync();
        public virtual T Find(params object[] key) => _entity.Find(key);

        public virtual T SingleOrDefault() => _entity.SingleOrDefault();
        public virtual T SingleOrDefault(Expression<Func<T, bool>> where) => _entity.AsNoTracking().Where(where).SingleOrDefault();
        public virtual async Task<T> SingleOrDefaultAsync() => await _entity.SingleOrDefaultAsync();

        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> where) => await _entity.AsNoTracking().Where(where).SingleOrDefaultAsync();
        public virtual ICollection<T> FindAll(Expression<Func<T, bool>> where) => _entity.Where(where).ToList();
        public virtual async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> where) => await _entity.Where(where).ToListAsync();
        public virtual async Task<T> FindAsync(params object[] key) => await _entity.FindAsync(key);
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> where)
        {
            IQueryable<T> query = _entity.Where(where);
            return query;
        }
        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> where) => await _entity.Where(where).ToListAsync();

        public virtual IEnumerable<T> GetAll() => _entity.ToList();
        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> where) => _entity.Where(where).ToList();

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _entity.ToListAsync();
        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where) => await _entity.Where(where).ToListAsync();

        public virtual IQueryable<T> Query() => _entity.AsNoTracking().AsQueryable();
        public virtual IQueryable<T> Query(Expression<Func<T, bool>> where) => _entity.AsNoTracking().Where(where).AsQueryable();

        public virtual void Save() => _context.SaveChanges();
        public virtual async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
