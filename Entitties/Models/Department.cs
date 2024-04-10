using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitties.Models
{
    public class Department
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Department Name is a required field.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Department Description is a required field.")]
        public string Description { get; set; }

        public Product product { get; set; }
        public List<Worker> workers { get; set; }
    }
}
