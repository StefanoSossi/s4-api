

using System.ComponentModel.DataAnnotations;

namespace s4.Data.Models
{
    public class Class : Entity
    {
        [Required]
        public required string Code { get; set; }

        [Required]
        [StringLength(350)]
        public required string Title { get; set; }
        public string? Description { get; set; }

    }
}
