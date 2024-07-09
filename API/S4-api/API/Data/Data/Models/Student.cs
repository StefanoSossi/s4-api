

using System.ComponentModel.DataAnnotations;

namespace s4.Data.Models
{
    public class Student : Entity
    {

        [StringLength(350)]
        public string? LastName { get; set; }

        [Required]
        [StringLength(350)]
        public required string FirstName { get; set; }

    }
}
