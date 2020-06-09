using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMS.Models;
using WMS.Services;

namespace WMS.WebMVC.Controllers
{
    public class OutboundController : Controller
    {
        private OutboundServices CreateOutboundService()
        {
            //var userId = Guid.Parse(User.Identity.GetUserId());
            var outService = new OutboundServices();
            return outService;
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OutboundModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new OutboundServices();

            service.AddOutbound(model);

            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            var db = new InventoryServices();
            ViewBag.SerialNum = new SelectList(db.GetInventory().ToList(), "SerialNum", "Name");
            return View();
        }
        public ActionResult Get()
        {
            OutboundServices outService = CreateOutboundService();
            var services = outService.GetOutboundLoadNum();
            return View(services);
        }

        public ActionResult Index()
        {
            //var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new OutboundServices();
            var model = service.GetOutbound();
            return View(model);
        }

        [System.Web.Mvc.ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var outService = new OutboundServices();
            var model = outService.GetOutboundInstance(id);

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = new OutboundServices();
            service.DeleteItem(id);
            TempData["SaveResult"] = "Your item was deleted";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var db = new InventoryServices();
            ViewBag.SerialNum = new SelectList(db.GetInventory().ToList(), "SerialNum", "Name");
            var service = new OutboundServices();
            var entity = service.GetOutboundInstance(id);
            var model =
                new OutboundModel
                {
                    LoadNum = entity.LoadNum,
                    TruckNum = entity.TruckNum,
                    Fleet = entity.Fleet,
                    LoadItems = entity.LoadItems,
                    Dock = entity.Dock,
                    Type = entity.Type,
                    TrailerLength = entity.TrailerLength,
                    LoadDate = entity.LoadDate,
                    SerialNum = entity.SerialNum,
                    Cargo = entity.Cargo,
                    Quantity = entity.Quantity
                };
            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OutboundModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.LoadNum != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            //var service = CreateInventoryService();
            var service = new OutboundServices();

            if (service.UpdateOutbound(model))
            {
                TempData["SaveResult"] = "Your item was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be updated.");
            return View(model);
        }
    }
}