using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Urbagestion.Model.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Addresss { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
    }
}