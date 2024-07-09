using System.ComponentModel.DataAnnotations;

namespace s4.Data.Models
{
    public class Entity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
    }
}
