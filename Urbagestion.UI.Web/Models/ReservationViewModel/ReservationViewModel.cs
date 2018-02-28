using System;
using System.ComponentModel.DataAnnotations;

namespace Urbagestion.UI.Web.Models.ReservationViewModel
{
    public class ReservationViewModel
    {
        [Required(ErrorMessage = "Debe especificar una instalación")]
        public int FacilityId { get; set; }

        [Required(ErrorMessage = "Debe especificar una fecha de reserva")]
        public DateTime ReservationDate { get; set; }

        public string FacilityName { get; set; }


    }
}