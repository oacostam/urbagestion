using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Urbagestion.UI.Web.Models.FacilityViewModels
{
    [Serializable]
    public class FacilityIndexViewModel : ModelBase, IValidatableObject
    {
        [Required]
        [ConcurrencyCheck]
        [MinLength(5, ErrorMessageResourceName = "Facility_NameMinLen", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Precio")]
        public decimal? Price { get; set; }

        [Display(Name = "Apertura")]
        [Required] public TimeSpan OpensAt { get; set; } = new TimeSpan(9, 0, 0);

        [Display(Name = "Cierre")]
        [Required] public TimeSpan CloseAt { get; set; } = new TimeSpan(22, 0, 0);


        public new IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            foreach (var validationResult in base.Validate(validationContext))
            {
                yield return validationResult;
            }
            if (string.IsNullOrEmpty(Name))
                yield return new ValidationResult(Resource.SharedResources_Name_NotNull);
            if (OpensAt == TimeSpan.Zero)
                yield return new ValidationResult(Resource.SharedResource_OpensAt_NotNull);
            if (CloseAt == TimeSpan.Zero)
                yield return new ValidationResult(Resource.SharedResource_OpensAt_NotNull);
        }
    }
}