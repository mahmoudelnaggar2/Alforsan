using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// save changes to submit objects to db
        /// </summary>
        void Commit();

        /// <summary>
        /// transaction to wrap multiple saveChanges
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// to commit multiple saveChanges
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// to rollback multiple saveChanges
        /// </summary>
        void RollBackTransaction();
    }
}
