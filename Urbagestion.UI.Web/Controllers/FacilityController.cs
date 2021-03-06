﻿using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Urbagestion.Model.Bussines.Interfaces;
using Urbagestion.Model.Models;
using Urbagestion.UI.Web.Logging;
using Urbagestion.UI.Web.Models;
using Urbagestion.UI.Web.Models.FacilityViewModels;

namespace Urbagestion.UI.Web.Controllers
{
    [Authorize]
    public class FacilityController : BaseController
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
        [Authorize(Roles = Role.AdminRoleName)]
        public ActionResult Create(FacilityIndexViewModel viewModel)
        {
            if (ModelState.IsValid)
                try
                {
                    SetAuditFields(viewModel);
                    var facility = mapper.Map<FacilityIndexViewModel, Facility>(viewModel);
                    facilityManagement.Create(facility);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    logger.LogError(new EventId(LogEvents.CreateError), ex, ex.Message);
                    return View("Error");
                }

            return RedirectToAction("Create", viewModel);
        }


        [Authorize(Roles = Role.AdminRoleName)]
        public ActionResult Edit(int id)
        {
            var facilities = facilityManagement.GetById(id);
            var result = mapper.Map<Facility, FacilityIndexViewModel>(facilities);
            return View(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Role.AdminRoleName)]
        public ActionResult Edit(FacilityIndexViewModel facilityViewModel)
        {
            if (!ModelState.IsValid) return View(facilityViewModel);
            try
            {
                var facility = mapper.Map<Facility>(facilityViewModel);
                facilityManagement.Update(facility);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(new EventId(LogEvents.UpdateError), ex, ex.Message);
                return View("Error");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(FacilityIndexViewModel request)
        {
            if (request == null || !ModelState.IsValid) return View("Index");
            try
            {
                facilityManagement.Delete(request.Id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(new EventId(LogEvents.UpdateError), ex, ex.Message);
                return View("Error");
            }
        }
    }
}