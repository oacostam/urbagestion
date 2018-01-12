using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Urbagestion.Model.Bussines.Interfaces;
using Urbagestion.Model.Interfaces;
using Urbagestion.Model.Models;
using Urbagestion.UI.Web.Models;
using Urbagestion.UI.Web.Models.FacilityViewModels;
using Urbagestion.UI.Web.Models.ManageViewModels;
using Urbagestion.Util;

namespace Urbagestion.UI.Web.Controllers
{
    [Authorize]
    public class FacilityController : Controller
    {
        private readonly IFacilityManagement facilityManagement;

        public FacilityController(IFacilityManagement facilityManagement)
        {
            this.facilityManagement = facilityManagement;
        }
        
        public ActionResult Index(RequestBase request)
        {
            try
            {
                var facilities = facilityManagement.GetAll(request.Page, request.Size, out var total, request.SortField, request.Order);
                PageResult<FacilityIndexViewModel[]> result = new PageResult<FacilityIndexViewModel[]>(){Result = Mapper.Map<Facility[], FacilityIndexViewModel[]>(facilities), Total = total};
                return View(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                PageResult<IndexViewModel[]> result = new PageResult<IndexViewModel[]>() {ErrorMessage = e.Message};
                return View(result);
            }
            
        }

        // GET: Facility/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Facility/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Facility/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Facility/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Facility/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}