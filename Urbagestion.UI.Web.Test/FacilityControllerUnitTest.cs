using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Urbagestion.Model.Bussines.Interfaces;
using Urbagestion.Model.Models;
using Urbagestion.UI.Web.Controllers;
using Urbagestion.UI.Web.Models.FacilityViewModels;
using Urbagestion.UI.Web.Test.Class;
using Xunit;

namespace Urbagestion.UI.Web.Test
{
    public class FacilityControllerUnitTest
    {
        public FacilityControllerUnitTest()
        {
            var facilities = new List<Facility>
            {
                new Facility
                {
                    Id = 1
                }
            };
            var logger = new Mock<ILogger<FacilityController>>();
            var facilityManagement = new Mock<IFacilityManagement>();
            facilityManagement.Setup(s => s.Create(It.IsAny<Facility>())).Returns((Facility f) =>
            {
                var maxId = facilities.Any() ? facilities.Max(m => m.Id) : 0;
                f.Id = ++maxId;
                facilities.Add(f);
                return f;
            });
            var mapper = new Mock<IMapper>();
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
            facilityController = new FacilityController(facilityManagement.Object, mapper.Object, logger.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext {User = TestHelper.GetAdminClaimsPrincipal()}
                }
            };
        }


        private readonly FacilityController facilityController;

        [Fact]
        public void AddFacilityReturnToIndex()
        {
            //Setup
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
            //Act
            var actionResult = facilityController.Delete(new FacilityIndexViewModel {Id = 1});
            //Verify
            Assert.True(((RedirectToActionResult) actionResult).ActionName == nameof(facilityController.Index));
        }

        [Fact]
        public void DeleteWithNullReturnsToIndexView()
        {
            //Setup
            //Act
            var actionResult = facilityController.Delete(null);
            //Verify
            Assert.True(((ViewResult) actionResult).ViewName == "Index");
        }
    }
}