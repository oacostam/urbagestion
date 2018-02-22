using System;
using Microsoft.AspNetCore.Mvc;
using Urbagestion.UI.Web.Models;

namespace Urbagestion.UI.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected void SetAuditFields(ModelBase model)
        {
            if (string.IsNullOrEmpty(model.CreatedBy))
            {
                model.CreatedBy = User.Identity.Name;
                model.CreationdDate = DateTime.Now;
            }

            model.UpdatedBy = User.Identity.Name;
            model.UpdatedDate = DateTime.Now;
        }
    }
}