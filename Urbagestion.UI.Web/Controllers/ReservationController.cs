using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Urbagestion.Model.Bussines.Interfaces;
using Urbagestion.Model.Interfaces;
using Urbagestion.Model.Models;
using Urbagestion.UI.Web.Models.ReservationViewModel;

namespace Urbagestion.UI.Web.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IReservationManagement reservationManagement;
        private readonly IFacilityManagement facilityManagement;

        public ReservationController(IReservationManagement reservationManagement, IFacilityManagement facilityManagement)
        {
            this.reservationManagement = reservationManagement ?? throw new ArgumentNullException(nameof(reservationManagement));
            this.facilityManagement = facilityManagement ?? throw new ArgumentNullException(nameof(facilityManagement));
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create(ReservationViewModel model)
        {
            Facility facility = facilityManagement.GetById(model.FacilityId);
            model.FacilityName = facility.Name;
            return View(model);
        }
    }
}