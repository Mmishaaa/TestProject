using System.ComponentModel.DataAnnotations;

namespace Entitties.Models
{
    public class Worker
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Worker FirstName is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the FirstName is 30 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Worker LastName is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the LastName is 30 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Age is a required field.")]
        public int Age { get; set; }

        public List<Department> Departments { get; set; }

    }
}
