using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Urbagestion.Model.Common;

namespace Urbagestion.Model.Models
{
    public class Facility : Entity
    {
        [MaxLength(50)] 
        [Required] 
        public string Name { get; set; }

        [Range(0, int.MaxValue)] 
        [Required] 
        public decimal? Price { get; set; }

        public TimeSpan OpensAt { get; set; }

        public TimeSpan CloseAt { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}