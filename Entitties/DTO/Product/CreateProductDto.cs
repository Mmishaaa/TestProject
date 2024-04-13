using Entities.DTO.Product;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class CreateProductDto : ProductManipulationDto
    {
       /* [Required(ErrorMessage = "Product Name is a required field.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product Description is a required field.")]
        public string Description { get; set; }

        [Range(0.001, double.MaxValue, ErrorMessage = "Weight is required and it can't be lower than 0.001 kg")]
        public double Weight { get; set; }*/
    }
}
