using Entitties.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.Product
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public double Weight { get; set; }

    }
}
