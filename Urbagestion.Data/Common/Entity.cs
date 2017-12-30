using System;
using System.ComponentModel.DataAnnotations;
using Urbagestion.Model.Interfaces;

namespace Urbagestion.Model.Common
{
    public abstract class Entity : IHasIdentity, IAuditableEntity
    {
        [Key]
        public virtual int Id { get; set; } = 0;

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public DateTime CreationdDate { get; set; } = DateTime.Now;
        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public string UpdatedBy { get; set; }
    }
}