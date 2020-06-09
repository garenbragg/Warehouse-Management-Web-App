using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Dynamic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using WMS.Data;
using WMS.Models;

namespace WMS.Services
{
    public class InventoryServices
    {
        //private readonly int _invUserId;
        
        //public InventoryServices(int invUserId)
        //{
        //    _invUserId = invUserId;
        //}

        public int GenerateSerial()
        {
            var random = new Random();
            string serialstring = string.Empty;
            for (int i =0; i < 10; i++)
            {
                serialstring = String.Concat(serialstring, random.Next(10).ToString());
            }
            int serialnum = Int32.Parse(serialstring);
            return serialnum;
        }

        public bool AddInventory(InventoryModel model)
        {
            var entity = new Inventory()
            {
                SerialNum = model.SerialNum,
                ItemNum = model.ItemNum,
                Name = model.Name,
                Location = model.Location,
                Quantity = model.Quantity,
                ArrivalDate = model.ArrivalDate,
                ExpirationDate = model.ExpirationDate
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.inventories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public InventoryModel GetInventorySerial(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.inventories.Single(e => e.SerialNum == id);
                return new InventoryModel
                {
                    SerialNum = entity.SerialNum,
                    ItemNum = entity.ItemNum,
                    Name = entity.Name,
                    Location = entity.Location,
                    Quantity = entity.Quantity,
                    ArrivalDate = entity.ArrivalDate,
                    ExpirationDate = entity.ExpirationDate
                };                
            }
        }

        //Show items that expire in a given date range
        public IEnumerable<InventoryModel> GetInventoryExp(DateTime startdate, DateTime enddate)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.inventories.Where(e => ((e.ExpirationDate > startdate) && (e.ExpirationDate < enddate))).Select(e => new InventoryModel
                {
                    SerialNum = e.SerialNum,
                    ItemNum = e.ItemNum,
                    Name = e.Name,
                    Location = e.Location,
                    Quantity = e.Quantity,
                    ArrivalDate = e.ArrivalDate,
                    ExpirationDate = e.ExpirationDate
                });
                return query.ToArray();
            }
        }

        //Search by Name
        public IEnumerable<InventoryModel> GetInventoryName()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = new Inventory();
                var query = ctx.inventories.Where(e => e.Name == entity.Name).Select(e => new InventoryModel
                {
                    SerialNum = e.SerialNum,
                    ItemNum = e.ItemNum,
                    Name = e.Name,
                    Location = e.Location,
                    Quantity = e.Quantity,
                    ArrivalDate = e.ArrivalDate,
                    ExpirationDate = e.ExpirationDate
                });
                return query.ToArray();
            }
        }

        public IEnumerable<InventoryModel> GetInventory()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = new Inventory();
                var query = ctx.inventories.Select(e => new InventoryModel
                {
                    SerialNum = e.SerialNum,
                    ItemNum = e.ItemNum,
                    Name = e.Name,
                    Location = e.Location,
                    Quantity = e.Quantity,
                    ArrivalDate = e.ArrivalDate,
                    ExpirationDate = e.ExpirationDate
                });
                return query.ToArray();
            }
        }

        public InventoryModel GetInventoryForEdits()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = new Inventory();
                var query = ctx.inventories.Select(e => e.SerialNum == entity.SerialNum); //sequence contains no elements
                return new InventoryModel
                {
                    SerialNum = entity.SerialNum,
                    ItemNum = entity.ItemNum,
                    Name = entity.Name,
                    Location = entity.Location,
                    Quantity = entity.Quantity,
                    ArrivalDate = entity.ArrivalDate,
                    ExpirationDate = entity.ExpirationDate
                };
            }
        }

        //Get Inventory by Item Number
        public IEnumerable<InventoryModel> GetInventoryItemNum()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = new Inventory();
                var query = ctx.inventories.Where(e => e.ItemNum == entity.ItemNum).Select(e => new InventoryModel
                {
                    SerialNum = e.SerialNum,
                    ItemNum = e.ItemNum,
                    Name = e.Name,
                    Location = e.Location,
                    Quantity = e.Quantity,
                    ArrivalDate = e.ArrivalDate,
                    ExpirationDate = e.ExpirationDate
                });
                return query.ToArray();
            }
        }

        //Search Inventory by Arrival Date

        public IEnumerable<InventoryModel> GetInventorybyArrival(DateTime startdate, DateTime enddate)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.inventories.Where(e => ((e.ArrivalDate > startdate) && (e.ArrivalDate < enddate))).Select(e => new InventoryModel
                {
                    SerialNum = e.SerialNum,
                    ItemNum = e.ItemNum,
                    Name = e.Name,
                    Location = e.Location,
                    ArrivalDate = e.ArrivalDate,
                    ExpirationDate = e.ExpirationDate
                });
                return query.ToArray();
            }
        }

        public bool UpdateInventory(InventoryModel model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .inventories
                        .Single(e => e.SerialNum == model.SerialNum);// && e.ItemNum == model.ItemNum); //contains no elements .Single

                entity.ItemNum = model.ItemNum;
                entity.Name = model.Name;
                entity.Location = model.Location;
                entity.ArrivalDate = model.ArrivalDate;
                entity.ExpirationDate = model.ExpirationDate;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteItem(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .inventories
                    .Single(e => e.SerialNum == id);
                ctx.inventories.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }

    }
}