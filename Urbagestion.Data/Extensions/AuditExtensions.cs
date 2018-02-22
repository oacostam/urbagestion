using System;
using System.Security.Principal;
using Urbagestion.Model.Interfaces;

namespace Urbagestion.Model.Extensions
{
    public static class AuditExtensions
    {
        public static void SetAuditFields(this IPrincipal principal, IAuditableEntity entity)
        {
            if (!(entity is IAuditableEntity auditable)) return;
            auditable.UpdatedDate = DateTime.Now;
            auditable.UpdatedBy = principal.Identity.Name;
            if (string.IsNullOrEmpty(auditable.CreatedBy)) 
                auditable.CreatedBy = principal.Identity.Name;
            if(auditable.CreationdDate == DateTime.MinValue)
                auditable.CreationdDate = DateTime.Now;
        }
    }
}