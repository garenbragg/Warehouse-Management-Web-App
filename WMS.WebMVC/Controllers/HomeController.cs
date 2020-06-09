using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMS.Services;

namespace WMS.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var service = new InventoryServices();
            var model = service.GetInventory();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Alpha version of a warehouse management system.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}