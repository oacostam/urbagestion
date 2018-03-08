using System;
using System.Collections.Generic;
using System.Linq;

namespace Urbagestion.Model.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();

        IEnumerable<T> Find<T, TKey>(Func<T, bool> filter, Func<T, TKey> orderBy = null) where T : class, IHasIdentity;

        T GetById<T>(int id) where T : class, IHasIdentity;

        T Update<T>(T entity) where T : class, IHasIdentity;

        int Count<T>(Func<T, bool> filter = null) where T : class, IHasIdentity;

        void Delete<T>(T entity) where T : class, IHasIdentity;

        T Add<T>(T entity) where T : class, IHasIdentity;
        
        IQueryable<T> GetEntitySet<T>() where T : class, IHasIdentity, IAuditableEntity;
    }
}