using System.ComponentModel.DataAnnotations;

namespace Entities.DTO.Worker
{
    public class CreateWorkerDtoForDepartment
    {
        [Required(ErrorMessage = "FirstName is a required field.")]

        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is a required field.")]
        public string LastName { get; set; }
        [Range(1, 100, ErrorMessage = "Age is required and it can't be lower than 0 and bigger than 100")]

        public int Age { get; set; }

    }
}
