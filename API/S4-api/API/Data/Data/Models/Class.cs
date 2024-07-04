

using System.ComponentModel.DataAnnotations;

namespace s4.Data.Models
{
    public class Class : Entity
    {
        [Key]
        [Required]
        public Guid Code { get; set; }

        [Required]
        [StringLength(350)]
        public required string Title { get; set; }
        public string? Description { get; set; }

        public ICollection<StudentClass>? StudentClasses { get; set; }

    }
}
