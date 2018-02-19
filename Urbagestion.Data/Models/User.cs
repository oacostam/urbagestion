using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Urbagestion.Model.Interfaces;

namespace Urbagestion.Model.Models
{
    [Table("Users")]
    public class User : IdentityUser<int>, IHasIdentity
    {
        public bool IsCoordinator { get; set; } = false;

        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();

        public virtual ICollection<UserGroup> UserGroup { get; set; } = new HashSet<UserGroup>();

        [Required]
        public string Address { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public string Name { get; set; }

        [Required]
        public string MiddleName { get; set; }
    }
}