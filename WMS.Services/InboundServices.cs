using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Data;
using WMS.Models;

namespace WMS.Services
{
    public class InboundServices
    {
        //private readonly Guid _inbUserId;
        //private readonly int _loadnum;
        //public InboundServices(int loadnum)
        //{
        //    _loadnum = loadnum;
        //}
        public bool AddInbound(InboundModel model)
        {
            var entity = new Inbound()
            {
                LoadNum = model.LoadNum,
                TruckNum = model.TruckNum,
                Fleet = model.Fleet,
                LoadItems = model.LoadItems,
                Dock = model.Dock,
                Type = model.Type,
                TrailerLength = model.TrailerLength,
                ArrivalDate = model.ArrivalDate,
                SerialNum = model.SerialNum,
                Cargo = model.Cargo,
                Quantity = model.Quantity
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.inbounds.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<InboundModel> GetInbound()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = new Inbound();
                var query = ctx.inbounds.Select(e => new InboundModel
                {
                    LoadNum = e.LoadNum,
                    TruckNum = e.TruckNum,
                    Fleet = e.Fleet,
                    LoadItems = e.LoadItems,
                    Dock = e.Dock,
                    Type = e.Type,
                    TrailerLength = e.TrailerLength,
                    ArrivalDate = e.ArrivalDate,
                    SerialNum = e.SerialNum,
                    Cargo = e.Cargo,
                    Quantity = e.Quantity
                });
                return query.ToArray();
            }
        }

        public IEnumerable<InboundModel> GetInboundLoadNum()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = new Inbound();
                var query = ctx.inbounds.Where(e => e.LoadNum == entity.LoadNum).Select(e => new InboundModel
                {
                    LoadNum = e.LoadNum,
                    TruckNum = e.TruckNum,
                    Fleet = e.Fleet,
                    LoadItems = e.LoadItems,
                    Dock = e.Dock,
                    Type = e.Type,
                    TrailerLength = e.TrailerLength,
                    ArrivalDate = e.ArrivalDate,
                    SerialNum = e.SerialNum,
                    Cargo = e.Cargo,
                    Quantity = e.Quantity
                });
                return query.ToArray();
            }

        }
        public IEnumerable<InboundModel> GetInboundbyArrival(DateTime startdate, DateTime enddate)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.inbounds.Where(e => ((e.ArrivalDate > startdate) && (e.ArrivalDate < enddate))).Select(e => new InboundModel
                {
                    LoadNum = e.LoadNum,
                    TruckNum = e.TruckNum,
                    Fleet = e.Fleet,
                    LoadItems = e.LoadItems,
                    Dock = e.Dock,
                    Type = e.Type,
                    TrailerLength = e.TrailerLength,
                    ArrivalDate = e.ArrivalDate,
                    SerialNum = e.SerialNum,
                    Cargo = e.Cargo,
                    Quantity = e.Quantity
                });
                return query.ToArray();
            }
        }
        public InboundModel GetInboundInstance(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.inbounds.Single(e => e.LoadNum == id);
                return new InboundModel
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
            }
        }

        public bool DeleteItem(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .inbounds
                    .Single(e => e.LoadNum == id);
                ctx.inbounds.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public bool UpdateInbound(InboundModel model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .inbounds
                        .Single(e => e.LoadNum == model.LoadNum);
                entity.LoadNum = model.LoadNum;
                entity.TruckNum = model.TruckNum;
                entity.Fleet = model.Fleet;
                entity.LoadItems = model.LoadItems;
                entity.Dock = model.Dock;
                entity.Type = model.Type;
                entity.TrailerLength = model.TrailerLength;
                entity.ArrivalDate = model.ArrivalDate;
                entity.SerialNum = model.SerialNum;
                entity.Cargo = model.Cargo;
                entity.Quantity = model.Quantity;

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
