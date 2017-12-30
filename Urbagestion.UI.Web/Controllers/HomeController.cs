using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Urbagestion.UI.Web.Models;

namespace Urbagestion.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = Resource.HomeController_About;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = Resource.HomeController_Contact;

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}