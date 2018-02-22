using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
        public FacilityControllerUnitTest()
        {
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
            mapper.Setup(s => s.Map<FacilityIndexViewModel, Facility>(It.IsAny<FacilityIndexViewModel>())).Returns(
                (FacilityIndexViewModel m) =>
                {
                    Debug.Assert(m.CloseAt != null, "m.CloseAt != null");
                    Debug.Assert(m.OpensAt != null, "m.OpensAt != null");
                    return new Facility
                    {
                        CloseAt = m.CloseAt.Value,
                        Id = m.Id,
                        Price = m.Price,
                        Name = m.Name,
                        OpensAt = m.OpensAt.Value,
                        IsActive = m.IsActive
                    };
                });
        }


        private readonly Mock<ILogger<FacilityController>> logger;
        private readonly Mock<IFacilityManagement> facilityManagement;
        private readonly List<Facility> facilities;
        private readonly Mock<IMapper> mapper;


        [Fact]
        public void DeleteWithNullIdReturnsHttp400()
        {
            //Setup
            var facilityController = new FacilityController(facilityManagement.Object, mapper.Object, logger.Object);
            //Act
            var actionResult = facilityController.Delete(null);
            //Verify
            Assert.True(((StatusCodeResult) actionResult).StatusCode == (int) HttpStatusCode.BadRequest);
        }

        [Fact]
        public void AddFacilityReturnToIndex()
        {
            //Setup
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
            Assert.True(((RedirectToActionResult) actionResult).ActionName == nameof(facilityController.Index));
        }

        [Fact]
        public void DeleteFacilityReturnToIndex()
        {
            //Setup
            facilities.Add(new Facility
            {
                Id = 1
            });
            var facilityController = new FacilityController(facilityManagement.Object, mapper.Object, logger.Object);
            //Act
            var actionResult = facilityController.Delete(1);
            //Verify
            Assert.True(((RedirectToActionResult) actionResult).ActionName == nameof(facilityController.Index));
        }
    }
}