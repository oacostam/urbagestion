using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Urbagestion.Model.Common;
using Urbagestion.Model.Interfaces;

namespace Urbagestion.Model.Bussines.Common
{
    public abstract class BaseService<T> : IDisposable where T : Entity
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual T Create(T entity)
        {
            if (entity == null)
            {
                throw new ApplicationException();
            }
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

        public IEnumerable<T> GetAll(int page, int size, out int total)
        {
            total = UnitOfWork.GetDbSet<T>().Count();
            int skipRows = (page - 1) * size;
            return UnitOfWork.GetDbSet<T>().OrderBy(c => c.Id).Skip(skipRows).Take(size).ToList();
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
            if (disposing)
            {
                UnitOfWork?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}