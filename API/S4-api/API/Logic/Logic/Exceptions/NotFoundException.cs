namespace s4.Logic.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception innerException) : base(string.Format(message), innerException) { }
    }
}