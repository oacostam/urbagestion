using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Urbagestion.Model.Common;
using Urbagestion.Model.Interfaces;
using Urbagestion.Util;

namespace Urbagestion.Model.Bussines.Common
{
    public abstract class BaseService<T> : IDisposable where T : Entity
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual T Create(T entity)
        {
            if (entity == null) throw new ApplicationException();
            UnitOfWork.GetDbSet<T>().Add(entity);
            UnitOfWork.SetAdded(entity);
            UnitOfWork.Complete();
            return entity;
        }

        public virtual void Delete(T entity)
        {
            UnitOfWork.GetDbSet<T>().Attach(entity);
            UnitOfWork.SetDeleted(entity);
            UnitOfWork.Complete();
        }

        public T[] GetAll(int page, int size, out int total, string orderBy, SortOrder sortOrder)
        {
            total = UnitOfWork.GetDbSet<T>().Count();
            var skipRows = (page - 1) * size;
            var query = (IQueryable<T>) UnitOfWork.GetDbSet<T>();
            var pi = typeof(T).GetProperty(orderBy);
            if (pi != null)
                query = sortOrder == SortOrder.Asc
                    ? query.OrderBy(x => pi.GetValue(x, null))
                    : query.OrderByDescending(x => pi.GetValue(x, null));
            return query.Skip(skipRows).Take(size).ToArray();
        }

        public virtual T Update(T entity)
        {
            UnitOfWork.GetDbSet<T>().Attach(entity);
            UnitOfWork.SetModified(entity);
            UnitOfWork.Complete();
            return entity;
        }

        public T GetById(int id)
        {
            return UnitOfWork.GetDbSet<T>().First(u => u.Id == id);
        }

        public T SetActive(int id, bool active)
        {
            var user = UnitOfWork.GetDbSet<T>().First(u => u.Id == id);
            user.IsActive = active;
            UnitOfWork.EntityEntry(user).State = EntityState.Modified;
            UnitOfWork.Complete();
            return user;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing) UnitOfWork?.Dispose();
        }
    }
}