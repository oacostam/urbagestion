using System;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Urbagestion.Model.Bussines.Interfaces;
using Urbagestion.Model.Models;
using Urbagestion.UI.Web.Models;
using Urbagestion.UI.Web.Models.FacilityViewModels;

namespace Urbagestion.UI.Web.Controllers
{
    [Authorize]
    public class FacilityController : Controller
    {
        private readonly IFacilityManagement facilityManagement;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public FacilityController(IFacilityManagement facilityManagement, IMapper mapper, ILogger<FacilityController> logger)
        {
            this.facilityManagement = facilityManagement;
            this.mapper = mapper;
            this.logger = logger;
        }

        public ActionResult Index(RequestBase request)
        {
            try
            {
                var facilities = facilityManagement.GetAll(request.Page, request.Size, out var total, request.SortField,
                    request.Order);
                var result = new PageResult<FacilityIndexViewModel[]>
                {
                    Result = mapper.Map<Facility[], FacilityIndexViewModel[]>(facilities),
                    Total = total
                };
                return View(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, null, null);
                return StatusCode(500);
            }
        }

        // GET: Facility/Details/5
        public ActionResult Details(int id)
        {
            var facilities = facilityManagement.GetById(id);
            var result = mapper.Map<Facility, FacilityIndexViewModel>(facilities);
            return View(result);
        }

        // GET: Facility/Create
        public ActionResult Create()
        {
            var facilityModel = new FacilityIndexViewModel();
            return View(facilityModel);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FacilityIndexViewModel facilityViewModel)
        {
            if (ModelState.IsValid)
                try
                {
                    var facility = mapper.Map<FacilityIndexViewModel, Facility>(facilityViewModel);
                    facilityManagement.Create(facility);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, null, null);
                    return View();
                }

            return RedirectToAction("Create", facilityViewModel);
        }
        

        public ActionResult Edit(int id)
        {
            var facilities = facilityManagement.GetById(id);
            var result = mapper.Map<Facility, FacilityIndexViewModel>(facilities);
            return View(result);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FacilityIndexViewModel facilityViewModel)
        {
            try
            {
                throw new NotImplementedException();
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Facility/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Facility/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(RequestBase request)
        {
            try
            {
                if (!request.Id.HasValue)
                {
                    return StatusCode((int) HttpStatusCode.BadRequest);
                }
                facilityManagement.Delete(request.Id.Value);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Error");
            }
        }
    }
}