using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : Model.BaseModel
    {
        #region Properties
        private DBEntities dataContext;
        private readonly IDbSet<T> dbSet;
        private readonly Helpers.SecurityHelper _security;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected DBEntities DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }
        }
        #endregion

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
            _security = new Helpers.SecurityHelper();
        }

        #region Implementation
        public virtual T Add(T entity)
        {
            entity.CreatedBy = _security.getUserIDFromToken();
            entity.CreationDate = DateTime.Now;
            entity.ModificationDate = DateTime.Now;
            entity.ModifiedBy = _security.getUserIDFromToken();
            return dbSet.Add(entity);
        }

        public virtual void Update(int id,T entity)
        {
            var local = dbSet.Find(id);
            if (local != null)
            {
                dataContext.Entry(local).State = EntityState.Detached;
            }
            entity.ModificationDate = DateTime.Now;
            entity.ModifiedBy = _security.getUserIDFromToken();
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }

        public virtual T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault<T>();
        }

        #endregion
    
    }
}
