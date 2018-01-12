using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Urbagestion.Model.Interfaces;

namespace Urbagestion.Model.Models
{
    [Table("Users")]
    public class User : IdentityUser<int>, IHasIdentity
    {
        public string Addresss { get; set; }

        public bool IsCoordinator { get; set; } = false;

        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();

        public virtual ICollection<UserGroup> UserGroup { get; set; } = new HashSet<UserGroup>();

        public string Street { get; set; }

        public int? StreetNumber { get; set; }

        public string Apartment { get; set; }

        public int? Floor { get; set; }

        public bool IsActive { get; set; } = true;
    }
}