using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private DBEntities dbContext;

        private DbContextTransaction cxtTransaction;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public DBEntities DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public void Commit()
        {
            DbContext.Commit();
        }
        /// <summary>
        /// transaction to wrap multiple saveChanges
        /// </summary>
        public void BeginTransaction()
        {
            cxtTransaction = DbContext.Database.BeginTransaction();

        }
        /// <summary>
        /// to commit multiple saveChanges
        /// </summary>
        public void CommitTransaction()
        {
            cxtTransaction.Commit();

        }
        /// <summary>
        /// to rollback multiple saveChanges
        /// </summary>
        public void RollBackTransaction()
        {
            cxtTransaction.Rollback();
        }
    }
}