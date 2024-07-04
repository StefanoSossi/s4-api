

using System.ComponentModel.DataAnnotations;

namespace s4.Data.Models
{
    public class Student : Entity
    {

        [Required]
        [StringLength(350)]
        public string? LastName { get; set; }

        [Required]
        [StringLength(350)]
        public required string FirstName { get; set; }

        public ICollection<StudentClass>? StudentClasses { get; set; }

    }
}
