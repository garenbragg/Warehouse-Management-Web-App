using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Data
{
    public class Inventory
    {
        [Key]
        public int SerialNum { get; set; }
        [Required]
        public int ItemNum { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int Quantity { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
