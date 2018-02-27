using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Moq;
using Urbagestion.Model.Bussines.Implementation;
using Urbagestion.Model.Common;
using Urbagestion.Model.Interfaces;
using Urbagestion.Model.Models;
using Urbagestion.UI.Web.Automapper;
using Xunit;

namespace Urbagestion.Model.Test
{
    public class ReservationManagementUnitTest
    {
        public ReservationManagementUnitTest()
        {
            Mapper.Initialize(expression => expression.AddProfiles(typeof(MappingProfile)));
            _reservations = new List<Reservation>();
            _facilities = new List<Facility>();
            _users = new List<User>();
        }

        private readonly List<Reservation> _reservations;
        private readonly List<Facility> _facilities;
        private readonly List<User> _users;

        private IUnitOfWork GetContext()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(s => s.GetEntitySet<Facility>()).Returns(() => _facilities.AsQueryable());
            unitOfWorkMock.Setup(s => s.GetEntitySet<Reservation>()).Returns(() => _reservations.AsQueryable());
            unitOfWorkMock.Setup(s => s.GetEntitySet<User>()).Returns(() => _users.AsQueryable());
            unitOfWorkMock.Setup(s => s.Add(It.IsAny<Reservation>()))
                .Returns((Reservation r) => TestHelper.AddMock(r, _reservations));
            unitOfWorkMock.Setup(s => s.Complete());
            unitOfWorkMock.Setup(s => s.Update(It.IsAny<Facility>())).Returns((Facility f) => f);
            unitOfWorkMock.Setup(s => s.Delete(It.IsAny<Facility>()))
                .Callback<Facility>(c => _facilities.Remove(_facilities.First(f => f.Id == c.Id)));

            return unitOfWorkMock.Object;
        }


        [Fact]
        public void TwoReservationsSameDayThrowsException()
        {
            Reservation r1 = new Reservation(){ReservationDate = DateTime.Now};
            Reservation r2 = new Reservation(){ReservationDate = DateTime.Now};
            using (IUnitOfWork unitOfWork = GetContext())
            {
                using (ReservationManagement reservationManagement = new ReservationManagement(unitOfWork, TestHelper.GetGenericPrincipalAdmin(), Mapper.Instance))
                {
                    reservationManagement.Add(r1);
                    Assert.Throws<BussinesException>(() => reservationManagement.Add(r2));
                }
            }
        }


        [Fact]
        public void AddReservationsHavingUnpayedThrowsException()
        {
            Reservation r1 = new Reservation(){ReservationDate = DateTime.Now.AddMonths(-1)};
            Reservation r2 = new Reservation(){ReservationDate = DateTime.Now};
            using (IUnitOfWork unitOfWork = GetContext())
            {
                using (ReservationManagement reservationManagement = new ReservationManagement(unitOfWork, TestHelper.GetGenericPrincipalAdmin(), Mapper.Instance))
                {
                    reservationManagement.Add(r1);
                    Assert.Throws<BussinesException>(() => reservationManagement.Add(r2));
                }
            }
        }
    }
}