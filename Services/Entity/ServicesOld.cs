//using Core.Domain.Base;
//using Core.Interfaces;
//using Data.Context;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Services.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace Services.Entity
//{

//    public class Services<T> : IServices<T> where T : BaseEntity
//    {
//        private IRepository<T> _repository;
//        private SmartContext _context;
//        private IUser _currentUser;
//        private string _currentUserId;
//        private string _currentUserEmail;
//        private int _businessEntityId;
//        private readonly ILogger _logger;



//        public Services(SmartContext context, IRepository<T> repository, IUser currentUser, ILogger<Services<T>> logger)
//        {
//            this._repository = repository;
//            this._context = context;
//            this._currentUser = currentUser;
//            this._currentUserId = _currentUser.Id();
//            this._currentUserEmail = _currentUser.Email();
//            this._logger = logger;
//            this._businessEntityId = _currentUser.BusinessEntityId();
//        }


//        public Expression<Func<T, bool>> InjectCurrentBusiness()
//        {
//            return p => (p.BusinessEntityId == _businessEntityId);
//        }


//        public int Count()
//        {
//            return this._repository.Query().Where(InjectCurrentBusiness()).Count();

//        }

//        public int Count(Expression<Func<T, bool>> where)
//        {
//            return this._repository.Query().Where(where).Where(InjectCurrentBusiness()).Count();
//        }



//        public T Find(params object[] key)
//        {
//            var data = this._repository.Find(key);

//            if (data != null)
//            {
//                if (data.BusinessEntityId != _businessEntityId)
//                {
//                    data = null;
//                }
//            }
//            return data;
//        }

//        public T Get(Expression<Func<T, bool>> where)
//        {
//            return this._repository.Query(where).Where(InjectCurrentBusiness()).FirstOrDefault();
//        }

//        public T Get()
//        {
//            return this._repository.Query(InjectCurrentBusiness()).FirstOrDefault();
//        }

//        public IEnumerable<T> GetAll()
//        {
//            return this._repository.GetAll(InjectCurrentBusiness());
//        }

//        public IEnumerable<T> GetAll(Expression<Func<T, bool>> where)
//        {
//            return this._repository.Query(where).Where(InjectCurrentBusiness()).ToList();
//        }

//        public IQueryable<T> Query()
//        {
//            return this._repository.Query().Where(InjectCurrentBusiness());
//        }
//        public IQueryable<T> Query(Expression<Func<T, bool>> where)
//        {
//            return this._repository.Query(where).Where(InjectCurrentBusiness());
//        }

//        public void Add(T entity)
//        {
//            entity.BusinessEntityId = this._businessEntityId;
//            this._repository.Add(entity);
//        }



//        public void Update(T entity)
//        {
//            entity.BusinessEntityId = this._businessEntityId;
//            try
//            {
//                typeof(T).GetProperty("ModifiedDate").SetValue(entity, System.DateTime.UtcNow);
//            }
//            catch (Exception)
//            {
//                ;
//            }

//            this._repository.Update(entity);

//        }


//        public void Delete(T entity)
//        {
//            entity.BusinessEntityId = this._businessEntityId;
//            this._repository.Delete(entity);
//        }

//        public void Delete(Expression<Func<T, bool>> where)
//        {
//            var _del = this._repository
//                .Query(where)
//                .Where(InjectCurrentBusiness())
//                .ToList();
//            _del.ForEach(a => this._repository.Delete(a));
//        }







//        public async Task<int> CountAsync(Expression<Func<T, bool>> where)
//        {
//            return await Task.Run(() =>
//            {
//                return _repository.Query(where).Where(a => a.BusinessEntityId == _businessEntityId).Count();
//            });

//        }

//        public async Task<int> CountAsync()
//        {
//            return await _repository.CountAsync(a => a.BusinessEntityId == _businessEntityId);
//        }

//        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where)
//        {
//            return await Task.Run(() =>
//            {
//                return GetAll(where);
//            });
//        }

//        public async Task<IEnumerable<T>> GetAllAsync()
//        {
//            return await _repository.GetAllAsync(InjectCurrentBusiness());
//        }

//        public async Task<T> GetAsync()
//        {
//            return await Task.Run(() =>
//            {
//                return Get();
//            });




//        }

//        public async Task<T> GetAsync(Expression<Func<T, bool>> where)
//        {
//            return await Task.Run(() =>
//            {
//                return Get(where);
//            });
//        }

//        public async Task<T> FindAsync(params object[] key)
//        {
//            return await Task.Run(() =>
//            {
//                return Find(key);
//            });
//        }

//        public async Task<int> DeleteAsync(Expression<Func<T, bool>> where)
//        {
//            return await Task.Run(() =>
//            {
//                Delete(where);
//                return 0;
//            });
//        }

//        public async Task<int> DeleteAsync(T entity)
//        {
//            return await Task.Run(() =>
//            {
//                Delete(entity);
//                return 0;
//            });
//        }

//        public async Task<T> AddAsync(T entity)
//        {
//            entity.BusinessEntityId = 0;// _currentUserId;
//            return await _repository.AddAsync(entity);
//        }

//        public async Task<T> UpdateAsync(T entity)
//        {
//            entity.BusinessEntityId = 0;// _currentUserId;
//            return await _repository.UpdateAsync(entity);
//        }

//        public async Task<IQueryable<T>> QueryAsync()
//        {
//            return await Task.Run(() =>
//            {
//                return Query();
//            });
//        }

//        public async Task<IQueryable<T>> QueryAsync(Expression<Func<T, bool>> where)
//        {
//            return await Task.Run(() =>
//            {
//                return Query(where);
//            });
//        }
//    }




//    public class LoggingEvents
//    {
//        public const int GENERATE_ITEMS = 1000;
//        public const int LIST_ITEMS = 1001;
//        public const int GET_ITEM = 1002;
//        public const int INSERT_ITEM = 1003;
//        public const int UPDATE_ITEM = 1004;
//        public const int DELETE_ITEM = 1005;

//        public const int GET_ITEM_NOTFOUND = 4000;
//        public const int UPDATE_ITEM_NOTFOUND = 4001;
//    }


//}
