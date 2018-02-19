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
        public bool IsActive { get; set; } = true;

       

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Id < 0)
            {
                yield return new ValidationResult(Resource.ModelBase_IdMinusCero);
            }
        }
    }
}