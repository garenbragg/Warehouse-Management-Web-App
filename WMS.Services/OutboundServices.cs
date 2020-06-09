using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Data;
using WMS.Models;

namespace WMS.Services
{
    public class OutboundServices
    {
        //private readonly Guid _outUserId;
        //public OutboundServices(Guid outUserId)
        //{
        //    _outUserId = outUserId;
        //}
        public bool AddOutbound(OutboundModel model)
        {
            var entity = new Outbound()
            {
                LoadNum = model.LoadNum,
                TruckNum = model.TruckNum,
                Fleet = model.Fleet,
                LoadItems = model.LoadItems,
                Dock = model.Dock,
                Type = model.Type,
                TrailerLength = model.TrailerLength,
                LoadDate = model.LoadDate,
                SerialNum = model.SerialNum,
                Cargo = model.Cargo,
                Quantity = model.Quantity
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.outbounds.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<OutboundModel> GetOutbound()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = new Outbound();
                var query = ctx.outbounds.Select(e => new OutboundModel
                {
                    LoadNum = e.LoadNum,
                    TruckNum = e.TruckNum,
                    Fleet = e.Fleet,
                    LoadItems = e.LoadItems,
                    Dock = e.Dock,
                    Type = e.Type,
                    TrailerLength = e.TrailerLength,
                    LoadDate = e.LoadDate,
                    SerialNum = e.SerialNum,
                    Cargo = e.Cargo,
                    Quantity = e.Quantity
                });
                return query.ToArray();
            }
        }

        public IEnumerable<OutboundModel> GetOutboundLoadNum()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = new Outbound();
                var query = ctx.outbounds.Where(e => e.LoadNum == entity.LoadNum).Select(e => new OutboundModel
                {
                    LoadNum = e.LoadNum,
                    TruckNum = e.TruckNum,
                    Fleet = e.Fleet,
                    LoadItems = e.LoadItems,
                    Dock = e.Dock,
                    Type = e.Type,
                    TrailerLength = e.TrailerLength,
                    LoadDate = e.LoadDate,
                    SerialNum = e.SerialNum,
                    Cargo = e.Cargo,
                    Quantity = e.Quantity
                });
                return query.ToArray();
            }

        }
        public IEnumerable<OutboundModel> GetOutboundbyArrival(DateTime startdate, DateTime enddate)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.outbounds.Where(e => ((e.LoadDate > startdate) && (e.LoadDate < enddate))).Select(e => new OutboundModel
                {
                    LoadNum = e.LoadNum,
                    TruckNum = e.TruckNum,
                    Fleet = e.Fleet,
                    LoadItems = e.LoadItems,
                    Dock = e.Dock,
                    Type = e.Type,
                    TrailerLength = e.TrailerLength,
                    LoadDate = e.LoadDate,
                    SerialNum = e.SerialNum,
                    Cargo = e.Cargo,
                    Quantity = e.Quantity
                });
                return query.ToArray();
            }
        }

        public OutboundModel GetOutboundInstance(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.outbounds.Single(e => e.LoadNum == id);
                return new OutboundModel
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
            }
        }

        public bool UpdateOutbound(OutboundModel model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .outbounds
                        .Single(e => e.LoadNum == model.LoadNum);
                entity.LoadNum = model.LoadNum;
                entity.TruckNum = model.TruckNum;
                entity.Fleet = model.Fleet;
                entity.LoadItems = model.LoadItems;
                entity.Dock = model.Dock;
                entity.Type = model.Type;
                entity.TrailerLength = model.TrailerLength;
                entity.LoadDate = model.LoadDate;
                entity.SerialNum = model.SerialNum;
                entity.Cargo = model.Cargo;
                entity.Quantity = model.Quantity;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteItem(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .outbounds
                    .Single(e => e.LoadNum == id);
                ctx.outbounds.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
