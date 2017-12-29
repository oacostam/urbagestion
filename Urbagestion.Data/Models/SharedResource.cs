using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Urbagestion.Model.Common;

namespace Urbagestion.Model.Models
{
    public class SharedResource : Entity, IValidatableObject
    {
        [Required] public string Name { get; set; }

        public decimal? Price { get; set; }

        [Required] public TimeSpan OpensAt { get; set; }

        [Required] public TimeSpan CloseAt { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Name))
                yield return new ValidationResult(Urbagestion_Model_Resource.Urbagestion_Model_Models_Name_NotNull);
            if (OpensAt == TimeSpan.Zero)
                yield return new ValidationResult(Urbagestion_Model_Resource
                    .Urbagestion_Model_Models_SharedResource_OpensAt_NotNull);
            if (CloseAt == TimeSpan.Zero)
                yield return new ValidationResult(Urbagestion_Model_Resource
                    .Urbagestion_Model_Models_SharedResource_OpensAt_NotNull);
        }
    }
}