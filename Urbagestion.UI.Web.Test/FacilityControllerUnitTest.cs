using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Urbagestion.Model.Bussines.Interfaces;
using Urbagestion.Model.Models;
using Urbagestion.UI.Web.Controllers;
using Urbagestion.UI.Web.Models;
using Urbagestion.UI.Web.Models.FacilityViewModels;
using Xunit;

namespace Urbagestion.UI.Web.Test
{
    public class FacilityControllerUnitTest
    {
        

        private Mock<ILogger<FacilityController>> logger;
        private Mock<IFacilityManagement> facilityManagement;
        private List<Facility> facilities;
        private Mock<IMapper> mapper;

        [Fact]
        public void CanAddFacility()
        {
            //Setup
            facilities = new List<Facility>();
            logger = new Mock<ILogger<FacilityController>>();
            facilityManagement = new Mock<IFacilityManagement>();
            facilityManagement.Setup(s => s.Create(It.IsAny<Facility>())).Returns((Facility f) =>
            {
                var maxId = facilities.Any() ? facilities.Max(m => m.Id) : 0;
                f.Id = ++maxId;
                facilities.Add(f);
                return f;
            });
            mapper = new Mock<IMapper>();
            mapper.Setup(s => s.Map<FacilityIndexViewModel, Facility>(It.IsAny<FacilityIndexViewModel>())).Returns((FacilityIndexViewModel m) => new Facility
            {
                CloseAt = m.CloseAt,
                Id = m.Id,
                Price = m.Price,
                Name = m.Name,
                OpensAt = m.OpensAt,
                IsActive = m.IsActive
            });

            var facilityController = new FacilityController(facilityManagement.Object, mapper.Object, logger.Object);
            //Act
            var actionResult = facilityController.Create(new FacilityIndexViewModel
            {
                CloseAt = new TimeSpan(22, 0, 0),
                Id = 0,
                Name = "Test",
                OpensAt = new TimeSpan(10, 0, 0),
                IsActive = true,
                Price = 0
            });
            //Verify
            Assert.True(((RedirectToActionResult)actionResult).ActionName == nameof(facilityController.Index));
        }


        [Fact]
        public void AddDuplicateFacility()
        {
            //Setup
            facilities = new List<Facility>();
            logger = new Mock<ILogger<FacilityController>>();
            facilityManagement = new Mock<IFacilityManagement>();
            facilityManagement.Setup(s => s.Create(It.IsAny<Facility>())).Returns((Facility f) =>
            {
                var maxId = facilities.Any() ? facilities.Max(m => m.Id) : 0;
                f.Id = ++maxId;
                facilities.Add(f);
                return f;
            });
            mapper = new Mock<IMapper>();
            mapper.Setup(s => s.Map<FacilityIndexViewModel, Facility>(It.IsAny<FacilityIndexViewModel>())).Returns((FacilityIndexViewModel m) => new Facility
            {
                CloseAt = m.CloseAt,
                Id = m.Id,
                Price = m.Price,
                Name = m.Name,
                OpensAt = m.OpensAt,
                IsActive = m.IsActive
            });

            var facilityController = new FacilityController(facilityManagement.Object, mapper.Object, logger.Object);
            //Act
            var actionResult = facilityController.Create(new FacilityIndexViewModel
            {
                CloseAt = new TimeSpan(22, 0, 0),
                Id = 0,
                Name = "Test",
                OpensAt = new TimeSpan(10, 0, 0),
                IsActive = true,
                Price = 0
            });
            //Verify
            Assert.True(((RedirectToActionResult)actionResult).ActionName == nameof(facilityController.Index));
        }

        [Fact]
        public void CanDeleteFacility()
        {
            //Setup
            facilities = new List<Facility>();
            logger = new Mock<ILogger<FacilityController>>();
            facilityManagement = new Mock<IFacilityManagement>();
            facilityManagement.Setup(s => s.Create(It.IsAny<Facility>())).Returns((Facility f) =>
            {
                var maxId = facilities.Any() ? facilities.Max(m => m.Id) : 0;
                f.Id = ++maxId;
                facilities.Add(f);
                return f;
            });
            mapper = new Mock<IMapper>();
            mapper.Setup(s => s.Map<FacilityIndexViewModel, Facility>(It.IsAny<FacilityIndexViewModel>())).Returns((FacilityIndexViewModel m) => new Facility
            {
                CloseAt = m.CloseAt,
                Id = m.Id,
                Price = m.Price,
                Name = m.Name,
                OpensAt = m.OpensAt,
                IsActive = m.IsActive
            });

            facilities.Add(new Facility()
            {
                Id = 1
            });
            var facilityController = new FacilityController(facilityManagement.Object, mapper.Object, logger.Object);
            //Act
            var actionResult = facilityController.Delete(new RequestBase(){Id = 1});
            //Verify
            Assert.True(((RedirectToActionResult)actionResult).ActionName == nameof(facilityController.Index));
        }
    }
}