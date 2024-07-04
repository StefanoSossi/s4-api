using s4.Data.Models;
using s4.Logic.Models.Validation;


namespace s4.Logic.Models
{
    public class StudentDto : ValidationData, IValidation 
    {
        public Guid Code { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }

        public ICollection<StudentClass>? StudentClasses { get; set; }

        public override bool IsValid()
        {
            if (Title == null)
            {
                AddError(new ValidationError
                {
                    Field = "Title",
                    Message = "The Title is required for a Class."
                });
            }
            return !Errors.Any();
        }

    }
}
