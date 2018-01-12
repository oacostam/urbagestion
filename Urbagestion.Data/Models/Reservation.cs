using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Urbagestion.Model.Common;

namespace Urbagestion.Model.Models
{
    public class Reservation : Entity
    {
        [Required] public User User { get; set; }
        
        [Required] public Facility Facility { get; set; }

        [Required] public DateTime ReservationDate { get; set; }

        [Required] public TimeSpan Starts { get; set; }

        [Required] public TimeSpan Ends { get; set; }

        public Payment Payment { get; set; }
        

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
            //if (User == null) yield return new ValidationResult(Urbagestion_Model_Resource.Reservation_User_NotNull);
            //if (Facility == null)
            //    yield return new ValidationResult(Urbagestion_Model_Resource.Reservation_Facility_NotNull);
            //if (ReservationDate < DateTime.Now)
            //    yield return new ValidationResult(Urbagestion_Model_Resource.Reservation_ReservationDate_NotInPast);
            //if (Starts >= Ends)
            //    yield return new ValidationResult(Urbagestion_Model_Resource.Reservation_StartsNotGreaterThanEnds);
        }
    }
}