using s4.Data.Models;

namespace s4.Logic.Models
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string? LastName { get; set; }
        public required string FirstName { get; set; }

        public ICollection<StudentClass>? StudentClasses { get; set; }

        
    }
}
