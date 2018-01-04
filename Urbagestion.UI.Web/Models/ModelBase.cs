using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Urbagestion.UI.Web.Models
{
    [Serializable]
    public abstract class ModelBase : IValidatableObject
    {
        [HiddenInput(DisplayValue = false)]
        [Required] public int Id { get; set; } = 0;

        [Required]
        [Display(ResourceType = typeof(Resource), Name = "ModelBase_IsActiveLabel")] 
        public bool IsActive { get; set; } = false;

        [Required]
        [Display(ResourceType = typeof(Resource), Name = "ModelBase_CreationdDateLabel")]
        public DateTime CreationdDate { get; set; } = DateTime.Now;

        [Required]
        [Display(ResourceType = typeof(Resource), Name = "ModelBase_CreatedByLabel")]
        public string CreatedBy { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resource), Name = "ModelBase_UpdatedDateLabel")]
        public DateTime UpdatedDate { get; set; }  = DateTime.Now;

        [Required] public string UpdatedBy { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(CreatedBy))
            {
                yield return new ValidationResult(Resource.ModelBase_CreatedByNotEmpty);
            }
            if (string.IsNullOrEmpty(UpdatedBy))
            {
                yield return new ValidationResult(Resource.ModelBase_CreatedByNotEmpty);
            }
        }
    }
}