using System;

namespace Insureme.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        long Id { get; }
        long Instances { get; }
        IDataContext DbContext { get; }
        int Save();
    }
}