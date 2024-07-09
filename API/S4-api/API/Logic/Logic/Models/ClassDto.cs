using s4.Logic.Models.Validation;


namespace s4.Logic.Models
{
    public class ClassDto : ValidationData, IValidation 
    {
        public Guid Id { get; set; }
        public required string Code { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }

        public IEnumerable<StudentDto>? Students { get; set; }

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
            if (Code == null)
            {
                AddError(new ValidationError
                {
                    Field = "Code",
                    Message = "The Code is required for a Class."
                });
            }
            return !Errors.Any();
        }

    }
}
