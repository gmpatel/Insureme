using System;
using Insureme.DataAccess.Interfaces;

namespace Insureme.DataAccess.Defaults
{
    public class UnitOfWork : IUnitOfWork
    {
        private static long counter;

        public IDataContext DbContext { get; private set; }

        public UnitOfWork(IDataContext dbContext)
        {
            Id = ++counter;
            DbContext = dbContext;
        }

        public long Id { get; private set; }
        public long Instances => counter;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Save()
        {
            return DbContext.SaveChanges();
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                    DbContext = null;
                }
            }
        }
    }
}