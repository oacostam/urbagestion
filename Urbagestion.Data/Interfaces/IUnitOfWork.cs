using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Urbagestion.Model.Common;

namespace Urbagestion.Model.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        EntityEntry<T> EntityEntry<T>(T entity) where T : Entity;

        int Complete();

        DbSet<T> GetDbSet<T>() where T : Entity;

        void SetModified(object entity);

        void SetDeleted(object entity);

        void SetAdded(object entity);
    }
}