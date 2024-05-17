﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostOfficeAPI.Models
{
    public class BagWithParcels : Bag
    {
        public List<Parcel>? Parcels { get; set; } = new List<Parcel>();
    }
}
