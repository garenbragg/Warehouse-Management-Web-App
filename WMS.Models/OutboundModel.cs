using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Data;

namespace WMS.Models
{
    public class OutboundModel
    {
        public int LoadNum { get; set; }
        public int TruckNum { get; set; }
        public bool Fleet { get; set; }
        public List<Inventory> LoadItems { get; set; }
        public string Dock { get; set; }
        public TruckType Type { get; set; }
        public float TrailerLength { get; set; }
        public DateTime LoadDate { get; set; }
        public int SerialNum { get; set; }
        public Inventory Cargo { get; set; }
        public int Quantity { get; set; }
    }
}
