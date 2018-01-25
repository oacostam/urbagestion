using System;
using System.Linq;
using System.Security.Principal;
using Urbagestion.Model.Common;
using Urbagestion.Model.Interfaces;
using Urbagestion.Model.Models;
using Urbagestion.Util;

namespace Urbagestion.Model.Bussines.Common
{
    public abstract class BaseService<T> : IDisposable where T : Entity
    {
        protected readonly IPrincipal Principal;
        protected readonly IUnitOfWork UnitOfWork;

        protected BaseService(IUnitOfWork unitOfWork, IPrincipal principal)
        {
            if (!principal.Identity.IsAuthenticated) throw new UnauthorizedAccessException();
            UnitOfWork = unitOfWork;
            Principal = principal;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual T Create(T entity)
        {
            if (entity == null) throw new ApplicationException();
            SetAuditFields(entity, true, Principal);
            UnitOfWork.GetDbSet<T>().Add(entity);
            UnitOfWork.SetAdded(entity);
            return entity;
        }


        private static void SetAuditFields(Entity entity, bool isBeenCreated, IPrincipal principal)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.CreatedBy = principal.Identity.Name;
            if (isBeenCreated) entity.CreationdDate = DateTime.Now;
        }

        public virtual void Delete(T entity, bool logicalDelete = true)
        {
            UnitOfWork.GetDbSet<T>().Attach(entity);
            if (logicalDelete)
            {
                entity.IsActive = false;
                SetAuditFields(entity, true, Principal);
            }
            else
            {
                if (Principal.IsInRole(Role.AdminRoleName)) UnitOfWork.SetDeleted(entity);
            }
        }

        public virtual T[] GetAll(int page, int size, out int total, string orderBy, SortOrder sortOrder)
        {
            total = UnitOfWork.GetDbSet<T>().Count();
            var skipRows = (page - 1) * size;
            var query = (IQueryable<T>) UnitOfWork.GetDbSet<T>();
            var propertyInfo = typeof(T).GetProperty(orderBy);
            if (propertyInfo != null)
                query = sortOrder == SortOrder.Asc
                    ? query.OrderBy(x => propertyInfo.GetValue(x, null))
                    : query.OrderByDescending(x => propertyInfo.GetValue(x, null));
            return query.Skip(skipRows).Take(size).ToArray();
        }

        public virtual T Update(T entity)
        {
            UnitOfWork.GetDbSet<T>().Attach(entity);
            SetAuditFields(entity, false, Principal);
            UnitOfWork.SetModified(entity);
            return entity;
        }

        public virtual T GetById(int id)
        {
            return UnitOfWork.GetDbSet<T>().First(u => u.Id == id);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing) UnitOfWork?.Dispose();
        }

        public virtual void Complete()
        {
            UnitOfWork.Complete();
        }
    }
}