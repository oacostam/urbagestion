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
        private DateTime updatedDate;
        private DateTime creationdDate;

        protected Entity()
        {
            creationdDate = DateTime.MinValue;
            updatedDate = DateTime.MinValue;
        }

        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Required] 
        public bool IsActive { get; set; } = true;

        [Timestamp]
        [ConcurrencyCheck]
        public byte[] RowVersion { get; set; }

        [Required]
        public DateTime CreationdDate
        {
            get => creationdDate;
            set
            {
                if(value == DateTime.MinValue)
                    throw new BussinesException($"{value} no es un valor válido para {nameof(CreationdDate)}.");
                creationdDate = value;
            }
        }

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

        [Required]
        [ConcurrencyCheck]
        public DateTime UpdatedDate
        {
            get => updatedDate;
            set
            {
                if(value == DateTime.MinValue)
                    throw new BussinesException($"{value} no es un valor válido para {nameof(UpdatedDate)}.");
                updatedDate = value;
            }
        }

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
    }
}