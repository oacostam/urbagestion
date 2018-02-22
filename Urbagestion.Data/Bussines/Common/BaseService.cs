using System;
using System.Linq;
using System.Security.Principal;
using AutoMapper;
using Urbagestion.Model.Common;
using Urbagestion.Model.Extensions;
using Urbagestion.Model.Interfaces;
using Urbagestion.Model.Models;
using Urbagestion.Util;

namespace Urbagestion.Model.Bussines.Common
{
    public abstract class BaseService<T> : IDisposable where T : class, IHasIdentity, IAuditableEntity
    {
        private readonly IPrincipal principal;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        protected IUnitOfWork UnitOfWork => unitOfWork;

        protected IPrincipal Principal => principal;

        protected BaseService(IUnitOfWork unitOfWork, IPrincipal principal, IMapper mapper)
        {
            this.principal = principal;
            if (!principal.Identity.IsAuthenticated) throw new UnauthorizedAccessException();
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        

        protected virtual T Create(T entity)
        {
            
            principal.SetAuditFields(entity);
            unitOfWork.Add(entity);
            return entity;
        }


        

        protected virtual void Delete(T entity)
        {
            try
            {
                unitOfWork.Delete(entity);
            }
            catch (Exception e)
            {
                throw new BussinesException("Ocurrió un error durante el borrado.", e);
            }
        }

        public virtual T[] GetAll(int page, int size, out int total, string orderBy, SortOrder sortOrder)
        {
            total = unitOfWork.GetEntitySet<T>().Count();
            var skipRows = (page - 1) * size;
            var query = unitOfWork.GetEntitySet<T>();
            query = query.Where(e => e.IsActive);
            var propertyInfo = typeof(T).GetProperty(orderBy);
            if (propertyInfo != null)
                query = sortOrder == SortOrder.Asc
                    ? query.OrderBy(x => propertyInfo.GetValue(x, null))
                    : query.OrderByDescending(x => propertyInfo.GetValue(x, null));
            return query.Skip(skipRows).Take(size).ToArray();
        }

        protected virtual T Update(T entity)
        {
            
            try
            {
                var dbEntity = GetById(entity.Id);
                mapper.Map(dbEntity, entity);
                principal.SetAuditFields(entity);
                unitOfWork.Update(entity);
                unitOfWork.Complete();
                return entity;
            }
            catch (Exception e)
            {
                throw new BussinesException("Ocurrió un error durante la actualización.", e);
            }
        }

        public virtual T GetById(int id)
        {
            var result = unitOfWork.GetEntitySet<T>().FirstOrDefault(u => u.Id == id);
            if(result == null)
            {
                throw new BussinesException("No se encontró el elemento solicitado.");
            }
            return result;
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing) unitOfWork?.Dispose();
        }

        protected virtual void Complete()
        {
            try
            {
                unitOfWork.Complete();
            }
            catch (Exception e)
            {
                throw new BussinesException("Ocurrió un error confirmando la transacción.", e);
            }
        }


        protected static void CheckNotNullAndAdminRigths(T entity, IPrincipal principal)
        {
            if (entity == null) 
                throw new BussinesException("No se puede actualizar una entidad nula");
            if(!principal.IsInRole(Role.AdminRoleName))
                throw new BussinesException("El usuario no tiene autorización para realizar la accion solicitada.");
        }
    }
}