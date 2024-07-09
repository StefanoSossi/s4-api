namespace s4.Logic.Models.Validation
{
    public class ValidationError
    {
        public required string Field { get; set; }

        public required string Message { get; set; }
    }

    public interface IValidation
    {
        bool IsValid();

        IEnumerable<ValidationError> GetErrors();
    }
}
