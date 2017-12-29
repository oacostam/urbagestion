using System;
using Urbagestion.Model.Interfaces;

namespace Urbagestion.Model.Common
{
    public abstract class Entity : IHasIdentity, IAuditableEntity
    {
        public virtual int Id { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public DateTime CreationdDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}