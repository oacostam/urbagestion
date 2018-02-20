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
        private string createdBy;
        private string updatedBy;

        protected Entity()
        {
            CreationdDate = DateTime.MinValue;
            UpdatedDate = DateTime.MinValue;
            Id = -1;
        }


        [Required] public DateTime CreationdDate { get; set; }

        [Required]
        public string CreatedBy
        {
            get => createdBy;
            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new BussinesException($"{nameof(CreatedBy)} no puede ser nulo.");
                createdBy = value;
            }
        }

        [Required] public DateTime UpdatedDate { get; set; }

        [Required]
        public string UpdatedBy
        {
            get => updatedBy;
            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new BussinesException($"{nameof(UpdatedBy)} no puede ser nulo.");
                updatedBy = value;
            }
        }

        [Key, Column(Order = 0)][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }

        [Required] public bool IsActive { get; set; } = true;
    }
}