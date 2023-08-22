namespace ConceptArchitect.BookManagement
{
    public class InvalidIdException<ID>:Exception
    {
        public ID Id { get; set; }
    }
}