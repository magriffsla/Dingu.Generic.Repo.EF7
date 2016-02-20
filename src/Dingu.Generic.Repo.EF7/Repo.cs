using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace Dingu.Generic.Repo.EF7
{
    public interface IRepo<T> where T:class
    {
        IQueryable<T> GetAll();
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T Get(int id);
    }
    public class Repo<TEntity> : IRepo<TEntity> where TEntity:class,IEntity
    {
        #region
        private DbContext _ctx;
        private DbSet<TEntity> _dbset
        {
            get
            {
                return _ctx.Set<TEntity>();
            }
        }
        #endregion
        public Repo(DbContext ctx)
        {
            _ctx = ctx;
        }
        public void Add(TEntity entity) {
            try
            {
                if (entity != null)
                {
                    _dbset.Add(entity);
                }
            }
            finally
            {
                _ctx.SaveChanges();
            }
        }
        public void Delete(TEntity entity) {
            try
            {
                if (entity != null)
                {
                    _dbset.Remove(entity);
                }
            }
            finally
            {
                _ctx.SaveChanges();
            }
        }
        public TEntity Get(int id)
        {
            var res = GetAll();
            return res.Where<TEntity>(e => e.Id == id).FirstOrDefault();
        }
        public IQueryable<TEntity> GetAll()
        {
            return _dbset.AsQueryable();
        }
        public void Update (TEntity entity)
        {
            try
            {
                if (entity != null)
                {
                    _ctx.Update(entity);
                }
            }
            finally
            {
                _ctx.SaveChanges();
            }
        }
    }
}
