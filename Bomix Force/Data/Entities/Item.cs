﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Status_art { get; set; }
        public string Description { get; set; }
        public int Id_Order { get; set; }
        public virtual Order Order { get; set; }
    }
}
