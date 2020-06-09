using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WMS.Models;
using WMS.Services;

namespace WMS.WebMVC.Controllers
{
    public class InventoryController : Controller
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
        private InventoryServices CreateInventoryService()
        {
            var itemId = GenerateSerial();
            var invService = new InventoryServices();
            return invService;
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = GenerateSerial();
            var service = new InventoryServices();

            service.AddInventory(model);

            return RedirectToAction("Index");
        }

        
        public ActionResult Get()
        {
            InventoryServices invService = CreateInventoryService();
            var services = invService.GetInventoryItemNum();
            return View(services);
        }

        //View Index
        public ActionResult Index()
        {
            var invId = GenerateSerial();
            var service = new InventoryServices();
            var model = service.GetInventory();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var service = new InventoryServices();
            var detail = service.GetInventorySerial(id);
            var model =
                new InventoryModel
                {
                    SerialNum = detail.SerialNum,
                    ItemNum = detail.ItemNum,
                    Name = detail.Name,
                    Location = detail.Location,
                    Quantity = detail.Quantity,
                    ArrivalDate = detail.ArrivalDate,
                    ExpirationDate = detail.ExpirationDate
                };
                return View(model);
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, InventoryModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if(model.SerialNum != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            //var service = CreateInventoryService();
            var service = new InventoryServices();

            if (service.UpdateInventory(model))
            {
                TempData["SaveResult"] = "Your item was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be updated.");
            return View(model);
        }

        [System.Web.Mvc.ActionName("Delete")]
        public ActionResult Delete (int id)
        {
            var invService = new InventoryServices();
            var model = invService.GetInventorySerial(id);

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = new InventoryServices();
            service.DeleteItem(id);
            TempData["SaveResult"] = "Your item was deleted";
            return RedirectToAction("Index");
        }
    }
}