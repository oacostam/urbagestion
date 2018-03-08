using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Urbagestion.Model.Common;

namespace Urbagestion.Model.Models
{
    public class Facility : Entity
    {
        private TimeSpan opensAt;
        private TimeSpan closeAt;
        private readonly List<Reservation> reservations;

        public Facility()
        {
            opensAt = new TimeSpan(0, 9, 0);
            closeAt = new TimeSpan(0, 22, 0);
            reservations = new List<Reservation>();
        }

        [MaxLength(50)] 
        [Required] 
        public string Name { get; set; }

        [Range(0, int.MaxValue)] 
        [Required] 
        public decimal? Price { get; set; }

        public TimeSpan OpensAt
        {
            get => opensAt;
            set
            {
                if (value > closeAt)
                {
                    throw new BussinesException("La hora de apertura no puede ser mayor que la de cierre.");
                }

                opensAt = value;
            }
        }

        public TimeSpan CloseAt
        {
            get => closeAt;
            set
            {
                if (value < closeAt)
                {
                    throw new BussinesException("La hora de cierre no puede ser menor que la de apertura.");
                }

                closeAt = value;
            }
        }

        public virtual IReadOnlyCollection<Reservation> Reservations => reservations.AsReadOnly();
    }
}