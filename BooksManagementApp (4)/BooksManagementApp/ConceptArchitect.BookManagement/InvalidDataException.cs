using System.Runtime.Serialization;

namespace ConceptArchitect.BookManagement
{
    public class InvalidDataException : Exception
    {
        public InvalidDataException()
        {
        }

        public InvalidDataException(string? message) : base(message)
        {
        }

        public InvalidDataException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}