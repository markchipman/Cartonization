﻿using Cartonization.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cartonization.Web.Models
{
    public class CartonizationViewModel
    {
        public List<Product> ProductsToPack { get; set; }

        public List<Carton> AvailableCartons { get; set; }

        public PackerResponse Response { get; set; }

    }
}