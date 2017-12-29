using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Urbagestion.Model.Models
{
    public class Reservation
    {
        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public Facility Facility { get; set; }

        [Required]
        public DateTime ReservationDate { get; set; }

        [Required]
        public TimeSpan Starts { get; set; }

        [Required]
        public TimeSpan Ends { get; set; }
        
        public decimal? PayedAmmount { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(User == null) yield return new ValidationResult(Urbagestion_Model_Resource.Reservation_User_NotNull);
            if (Facility == null) yield return new ValidationResult(Urbagestion_Model_Resource.Reservation_Facility_NotNull);
            if(ReservationDate < DateTime.Now) yield return new ValidationResult(Urbagestion_Model_Resource.Reservation_ReservationDate_NotInPast);
            if(Starts >= Ends) yield return new ValidationResult(Urbagestion_Model_Resource.Reservation_StartsNotGreaterThanEnds);
        }
    }
}
