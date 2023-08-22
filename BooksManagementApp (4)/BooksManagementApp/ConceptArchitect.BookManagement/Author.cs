namespace ConceptArchitect.BookManagement
{
    public class Author
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public override string ToString()
        {
            return $"Author[Id={Id} , Name={Name} ]";
        }

    }
}