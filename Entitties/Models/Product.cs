using System.ComponentModel.DataAnnotations;

namespace Entitties.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Product Name is a required field.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product Description is a required field.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Product Weight is a required field.")]
        public double Weight { get; set; }

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
