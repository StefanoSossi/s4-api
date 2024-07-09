

using System.ComponentModel.DataAnnotations;

namespace s4.Data.Models
{
    public class Student
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(350)]
        public string LastName { get; set; }

        [Required]
        [StringLength(350)]
        public string FirstName { get; set; }

        public ICollection<StudentClass> StudentClasses { get; set; }

    }
}
