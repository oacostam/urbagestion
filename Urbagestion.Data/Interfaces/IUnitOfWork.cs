using System;
using System.Linq;

namespace Urbagestion.Model.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();

        IQueryable<T> GetEntitySet<T>() where T : class, IHasIdentity;

        T Update<T>(T entity) where T : class, IHasIdentity;

        void Delete<T>(T entity) where T : class, IHasIdentity;

        T Add<T>(T entity) where T : class, IHasIdentity;
    }
}