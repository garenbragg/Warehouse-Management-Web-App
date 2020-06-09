﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Data
{
    public class Outbound
    {
        [Key]
        public int LoadNum { get; set; }
        public int TruckNum { get; set; }
        public bool Fleet { get; set; }
        public List<Inventory> LoadItems { get; set; }
        [Required]
        public string Dock { get; set; }
        public TruckType Type { get; set; }
        public float TrailerLength { get; set; }
        public DateTime LoadDate { get; set; }
        public int SerialNum { get; set; }
        [ForeignKey(nameof(SerialNum))]
        public Inventory Cargo { get; set; }
        public int Quantity { get; set; }
    }
}
