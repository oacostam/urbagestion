using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using AutoMapper;
using Moq;
using Urbagestion.Model.Bussines.Implementation;
using Urbagestion.Model.Common;
using Urbagestion.Model.Interfaces;
using Urbagestion.Model.Models;
using Xunit;

namespace Urbagestion.Model.Test
{
    public class FacilityManagementUnitTest
    {

        public FacilityManagementUnitTest(IMapper mapper)
        {
            this.mapper = mapper;
        }

        private readonly List<Facility> facilities = new List<Facility>();
        private readonly List<Reservation> reservations = new List<Reservation>();
        private readonly IMapper mapper;

        private static IPrincipal GetAdminPrincipal()
        {
            var identity = new GenericIdentity("Test");
            return new GenericPrincipal(identity, new[] {Role.AdminRoleName});
        }

        private IUnitOfWork GetContext()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(s => s.GetEntitySet<Facility>()).Returns(() => facilities.AsQueryable());
            unitOfWorkMock.Setup(s => s.GetEntitySet<Reservation>()).Returns(() => reservations.AsQueryable());
            unitOfWorkMock.Setup(s => s.Add(It.IsAny<Facility>())).Returns((Facility f) =>
            {
                var last = facilities.Any() ? facilities.Max(m => m.Id) : 0;
                f.Id = ++last;
                facilities.Add(f);
                return f;
            });
            unitOfWorkMock.Setup(s => s.Complete());
            unitOfWorkMock.Setup(s => s.Update(It.IsAny<Facility>())).Returns((Facility f) => f);
            unitOfWorkMock.Setup(s => s.Delete(It.IsAny<Facility>()))
                .Callback<Facility>(c => facilities.Remove(facilities.First(f => f.Id == c.Id)));

            return unitOfWorkMock.Object;
        }


        [Fact]
        public void AddDuplicateNameThrowsBussinesException()
        {
            using (var dbContext = GetContext())
            {
                using (var facilityManagement = new FacilityManagement(dbContext, GetAdminPrincipal(), mapper))
                {
                    facilityManagement.Create(new Facility {Name = "Test"});
                    Assert.Throws<BussinesException>(() => facilityManagement.Create(new Facility {Name = "Test"}));
                }
            }
        }

        [Fact]
        public void CanAddFacility()
        {
            using (var ctx = GetContext())
            {
                using (var facilityManagement = new FacilityManagement(ctx, GetAdminPrincipal(), mapper))
                {
                    var facility = new Facility {Name = "Test"};
                    facilityManagement.Create(facility);
                    Assert.True(ctx.GetEntitySet<Facility>().Any());
                }
            }
        }

        [Fact]
        public void CanDeleteFacilityWithoutReservations()
        {
            using (var dbContext = GetContext())
            {
                var facility = new Facility {Name = "Test", Id = 2};
                facilities.Add(facility);
                var facilityManagement = new FacilityManagement(dbContext, GetAdminPrincipal(), mapper);
                facilityManagement.Delete(facility.Id);
                Assert.False(facilities.Any());
            }
        }

        [Fact]
        public void DeleteFacilityWithReservationsPerformsLogicalDeletion()
        {
            var user = new User
            {
                Address = "Address",
                Email = "email@email.com",
                Name = "Name",
                UserName = "Username",
                MiddleName = "Middle"
            };
            var facility = new Facility {Name = "Test"};
            var reservation = new Reservation
            {
                CreationdDate = DateTime.Now,
                CreatedBy = "Test",
                Ends = new TimeSpan(20, 0, 0),
                ReservationDate = DateTime.Now,
                Starts = new TimeSpan(19, 0, 0),
                UpdatedBy = "Test",
                UpdatedDate = DateTime.Now,
                User = user,
                Facility = facility
            };
            facilities.Add(facility);
            reservations.Add(reservation);
            
            using (var dbContext = GetContext())
            {
                using (var facilityManagement = new FacilityManagement(dbContext, GetAdminPrincipal(), mapper))
                {
                    facilityManagement.Delete(facility.Id);
                    facility = facilityManagement.GetById(facility.Id);
                    Assert.False(facility.IsActive);
                }
            }
        }

        [Fact]
        public void GetByIdNonExistingIdThrowsBussinesException()
        {
            using (var dbContext = GetContext())
            {
                using (var facilityManagement = new FacilityManagement(dbContext, GetAdminPrincipal(), mapper))
                {
                    Assert.Throws<BussinesException>(() => facilityManagement.GetById(int.MaxValue));
                }
            }
        }
    }
}