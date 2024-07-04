using s4.Data.Models;
using s4.Logic.Models.Validation;

namespace s4.Logic.Models
{
    public class StudentDto : ValidationData, IValidation
    {
        public Guid Id { get; set; }
        public string? LastName { get; set; }
        public required string FirstName { get; set; }

        public ICollection<StudentClass>? StudentClasses { get; set; }

        public override bool IsValid()
        {
            if (FirstName == null)
            {
                AddError(new ValidationError
                {
                    Field = "FirstName",
                    Message = "The FirstName is required for a Student."
                });
            }
            return !Errors.Any();
        }
    }
}
