using System.Runtime.Serialization;

namespace ConceptArchitect.BookManagement
{
    public class DuplicateIdException<T> : Exception
    {
        public DuplicateIdException()
        {
        }

        public DuplicateIdException(string? message) : base(message)
        {
        }

        public DuplicateIdException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DuplicateIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public T Id { get; set; }
    }
}