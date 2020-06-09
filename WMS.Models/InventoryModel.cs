using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Models
{
    public class InventoryModel
    {
        public int SerialNum { get; set; }
        public int ItemNum { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Quantity { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
