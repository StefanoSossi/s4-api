
namespace s4.Data.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string message) : base(message) { }

        public DatabaseException(string message, Exception innerException) : base(string.Format(message), innerException) { }
    }
}