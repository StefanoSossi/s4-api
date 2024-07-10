using s4.Logic.Models.Validation;

namespace s4.Presentation.Exceptions
{
    public class BadRequestException : Exception
    {
        public readonly IDictionary<string, List<string>> Details;

        public BadRequestException(string message) : base(message) { }

        public BadRequestException(string message, Exception innerException) : base(string.Format(message), innerException) { }

        public BadRequestException(string message, IEnumerable<ValidationError> errors): base(message)
        {
            Details = new Dictionary<string, List<string>>();
            foreach (ValidationError error in errors)
            {
                List<string> messages;
                if (Details.TryGetValue(error.Field, out List<string> value))
                {
                    messages = value;
                }
                else
                {
                    messages = new List<string>();
                    Details[error.Field] = messages;
                }
                messages.Add(error.Message);
            }
        }
    }
}