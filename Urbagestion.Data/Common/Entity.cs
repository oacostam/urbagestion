using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Urbagestion.Model.Interfaces;

namespace Urbagestion.Model.Common
{
    /// <summary>
    ///     Base class for model. Set default values in in audit fields.
    /// </summary>
    public abstract class Entity : IHasIdentity, IAuditableEntity
    {
        protected Entity()
        {
            CreationdDate = DateTime.MinValue;
            UpdatedDate = DateTime.MinValue;
            Id = -1;
        }


        [Required] public DateTime CreationdDate { get; set; }

        [Required] public string CreatedBy { get; set; }

        [Required] public DateTime UpdatedDate { get; set; }

        [Required] public string UpdatedBy { get; set; }

        [Key, Column(Order = 0)][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }

        [Required] public bool IsActive { get; set; } = true;
    }
}