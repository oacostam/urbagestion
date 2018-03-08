using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Urbagestion.Model.Common;
using Urbagestion.Model.Interfaces;

namespace Urbagestion.Model.Models
{
    [Table("Users")]
    public class User : IdentityUser<int>, IHasIdentity, IAuditableEntity
    {
        public bool IsCoordinator { get; set; } = false;

        public virtual ICollection<Reservation> Reservations { get;} = new HashSet<Reservation>();

        public virtual ICollection<UserGroup> UserGroup { get;} = new HashSet<UserGroup>();

        public virtual ICollection<Payment> Payments { get; } = new HashSet<Payment>();

        [Required]
        public string Address { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public string Name { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public DateTime CreationdDate { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public string UpdatedBy { get; set; }

        [Timestamp] 
        [ConcurrencyCheck] 
        public byte[] RowVersion { get; } = null;

        public void AddPayment(Payment payment)
        {
            if (payment == null) throw new ArgumentNullException(nameof(payment));
            Payments.Add(payment);
        }

        public void BookFacility(Reservation reservation)
        {
            if (reservation == null) throw new ArgumentNullException(nameof(reservation));
            
        }
    }
}