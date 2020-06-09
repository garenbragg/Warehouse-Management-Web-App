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
    public class InboundController : Controller
    {
        public int GenerateSerial()
        {
            var random = new Random();
            string serialstring = string.Empty;
            for (int i = 0; i < 9; i++)
            {
                serialstring = String.Concat(serialstring, random.Next(10).ToString());
            }
            int serialnum = Int32.Parse(serialstring);
            return serialnum;
        }
        private InboundServices CreateInboundService()
        {
            //var userId = Guid.Parse(User.Identity.GetUserId());
            var serial = GenerateSerial();
            var invService = new InboundServices();
            return invService;
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InboundModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //var userId = Guid.Parse(User.Identity.GetUserId());
            var serial = GenerateSerial();
            var service = new InboundServices();

            service.AddInbound(model);

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
            InboundServices inbService = CreateInboundService();
            var services = inbService.GetInboundLoadNum();
            return View(services);
        }

        public ActionResult Index()
        {
            //var userId = Guid.Parse(User.Identity.GetUserId());
            //var serial = GenerateSerial();
            var service = new InboundServices();
            var model = service.GetInbound();
            return View(model);
        }

        [System.Web.Mvc.ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var inbService = new InboundServices();
            var model = inbService.GetInboundInstance(id);

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = new InboundServices();
            service.DeleteItem(id);
            TempData["SaveResult"] = "Your item was deleted";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            //Create Inventory Service creates a new ID!
            //var service = CreateInventoryService();
            var db = new InventoryServices();
            ViewBag.SerialNum = new SelectList(db.GetInventory().ToList(), "SerialNum", "Name");
            var service = new InboundServices();
            var entity = service.GetInboundInstance(id);
            var model =
                new InboundModel
                {
                    LoadNum = entity.LoadNum,
                    TruckNum = entity.TruckNum,
                    Fleet = entity.Fleet,
                    LoadItems = entity.LoadItems,
                    Dock = entity.Dock,
                    Type = entity.Type,
                    TrailerLength = entity.TrailerLength,
                    ArrivalDate = entity.ArrivalDate,
                    SerialNum = entity.SerialNum,
                    Cargo = entity.Cargo,
                    Quantity = entity.Quantity
                };
            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, InboundModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.LoadNum != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            //var service = CreateInventoryService();
            var service = new InboundServices();

            if (service.UpdateInbound(model))
            {
                TempData["SaveResult"] = "Your item was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be updated.");
            return View(model);
        }
    }
}