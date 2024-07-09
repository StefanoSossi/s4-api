
namespace s4.Logic.Models.Validation
{
    public abstract class ValidationData : IValidation
    {
        protected IEnumerable<ValidationError> Errors;
        protected ValidationData()
        {
            Errors = new List<ValidationError>();
        }
        public abstract bool IsValid();


        protected void AddError(ValidationError error)
        {
            ((List<ValidationError>)Errors).Add(error);
        }

        public IEnumerable<ValidationError> GetErrors()
        {
            return Errors;
        }
    }
}
