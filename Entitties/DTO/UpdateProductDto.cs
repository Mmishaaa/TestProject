﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class UpdateProductDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Weight { get; set; }
    }
}
