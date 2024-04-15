using System.ComponentModel.DataAnnotations;

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

        public List<Product> Products { get; set; }
        public List<Worker> Workers { get; set; }
    }
}
