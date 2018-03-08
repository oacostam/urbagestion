using System;
using System.Linq;
using System.Security.Principal;
using AutoMapper;
using Urbagestion.Model.Bussines.Common;
using Urbagestion.Model.Bussines.Interfaces;
using Urbagestion.Model.Common;
using Urbagestion.Model.Interfaces;
using Urbagestion.Model.Models;

namespace Urbagestion.Model.Bussines.Implementation
{
    public class ReservationManagement:BaseService<Reservation>, IReservationManagement
    {
        public ReservationManagement(IUnitOfWork unitOfWork, IPrincipal principal, IMapper mapper) : base(unitOfWork, principal, mapper)
        {
            
        }


        public Reservation Add(Reservation reservation)
        {
            if (reservation == null) throw new ArgumentNullException(nameof(reservation));
            CheckOnlyOnePerDay(reservation);
            CheckNotUnpayedReservations();
            Create(reservation);
            return reservation;
        }

        private void CheckNotUnpayedReservations()
        {
            var unpayed = UnitOfWork.GetEntitySet<Reservation>()
                .Any(r => r.Payment == null && r.ReservationDate <= DateTime.Today);
            if(unpayed)
                throw new BussinesException("No puede realizar nuevas reservas si tiene reservas pendientes de pago.");
        }

        private void CheckOnlyOnePerDay(Reservation reservation)
        {
            var other = UnitOfWork.GetEntitySet<Reservation>()
                .FirstOrDefault(r => r.ReservationDate.Date == reservation.ReservationDate.Date);
            if(other != null)
                throw new BussinesException("No se puede realizar más de una reserva por día");
        }
    }
}