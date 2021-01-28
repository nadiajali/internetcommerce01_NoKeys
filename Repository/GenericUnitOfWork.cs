using internetcommerce01.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace internetcommerce01.Repository
{
    public class GenericUnitOfWork : IDisposable
    {
        private db_internetcommerce01Entities dbEntity = new db_internetcommerce01Entities();
        public IRepository<TableEntityType> GetRepositoryInstance<TableEntityType>() where TableEntityType : class
        {
            return new GenericRepository<TableEntityType>(dbEntity);
        }

        public void SaveChanges()
        {
            dbEntity.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbEntity.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
    }
}