using ConceptArchitect.BookManagement;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Xunit;

namespace ConceptArchitect.BookManaement.Tests
{
    [Collection("Author Manager Specs")]
    public class AuthorManagerSpecs: IDisposable
    {
        AuthorManagerV2 authorManager;
        string validId = "vivek-dutta-mishra";
        string authorName = "Vivek Dutta Mishra";
        Author author1, author2, author3;
        string expectedText = "The Accursed God";
        int authorCount;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\MyWorks\Corporate\202307-ecolab-cs\books_test_db.mdf;Integrated Security=True;Connect Timeout=30";
        TestSetup setup;
        public AuthorManagerSpecs()
        {
            author1 = new Author() { Id = validId, Name = authorName, Biography = $"author of {expectedText}", Photo = "author.jpg", Email ="author1@email.com" };
            author2 = new Author() { Id = "id2", Name = "Name2", Biography = $"Biography of Name3", Photo = "author2.jpg", Email = "author2@email.com" };
            author3 = new Author() { Id = "id3", Name = "Name3", Biography = $"Biography of Name3", Photo = "author3.jpg", Email = "author3@email.com" };


            setup = new TestSetup() { ConnectionString = connectionString };
            setup.SetUpAuthors(author1, author2, author3);

            authorManager = new AuthorManagerV2(() =>
            {
                var connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            });
            
            authorCount = 3;
        }


        public void Dispose()
        {
            //will be called after each test.
            //we can drop our table here
        }



        [Fact]
        public void GetAllAuthorsReturnsAllAuthors()
        {
            

            var authors = authorManager.GetAllAuthors();

            Assert.Equal(authorCount, authors.Count);
        }

        [Fact]
        public void GetAuthorByIdReturnAuthorForValidId()
        {
            var author = authorManager.GetAuthorById(validId);

            Assert.NotNull(author);
            Assert.Equal(authorName, author.Name);

        }

        [Fact]
        public void GetAuthorByIdShouldFailForInvalidId()
        {
            var ex= Assert.Throws<InvalidIdException<string>>(() => authorManager.GetAuthorById("invalid-id"));

            Assert.Equal("invalid-id", ex.Id);
        }

        [Fact]
        public void SearchAuthorReturnsAuthorWithTextMatchingInNameOrBiography()
        {
          

            List<Author> authors = authorManager.Search(expectedText);

            Assert.Equal(1, authors.Count);
            Assert.Equal(authorName, authors[0].Name);
            Assert.Contains(expectedText, authors[0].Biography);
        }

        [Fact]
        public void SearchAuthorReturnsEmptyListForNoMatch()
        {
            

            List<Author> authors = authorManager.Search("Foo");

            Assert.Equal(0, authors.Count);
            
        }


        [Fact]
        public void GetAuthorCountReturnsAuthorCount()
        {
            int authorCount = authorManager.GetAuthorCount();

            Assert.Equal(authorCount, authorCount);


        }

        [Fact(
           // Skip ="To be tested in Phase 2"
            )]
        public void AddAuthorAddsValidAuthorWithUniqueIdToList()
        {
           
            var newId = "new-author";
            var name = "New Author";
            var author = new Author()
            {
                Id = newId,
                Name = name,
                Biography = "Biography of New Author",
                Photo="neauthor.png"
            };

            authorManager.AddAuthor(author);

            var dbAuthor = authorManager.GetAuthorById(newId);

            Assert.Equal(newId, dbAuthor.Id);
            
        }


        [Fact(
          //  Skip = "To be tested in Phase 2"
            )]
        public void AddAuthorFailsForDuplicateId()
        {
           
            var author = new Author()
            {
                Id = validId,
                Name = authorName,
                Biography = "Biography of New Author",
                Photo = "neauthor.png"
            };

            
            var ex= Assert.Throws<DuplicateIdException<string>>(()=> authorManager.AddAuthor(author));

            Assert.Equal(validId, ex.Id);

            

        }

        [Fact(
         //   Skip = "To be tested in Phase 2"
            )]
        public void AddAuthorFailsForInvalidAuthor()
        {
            
            string newId = "new-id";
            var author = new Author()
            {
                Id = newId,
                //Name = authorName, //author is invalid without Name
                Biography = "Biography of New Author",
                Photo = "neauthor.png"
            };


            var ex = Assert.Throws<InvalidDataException>(() => authorManager.AddAuthor(author));


        }


        [Fact(
           // Skip = "To be tested in Phase 2"
            )]
        public void RemoveAuthorFailsForInvalidId()
        {
            Assert.Throws<InvalidIdException<string>>(() => authorManager.RemoveAuthor("invalid-id"));


        }

        [Fact(
           // Skip = "To be tested in Phase 2"
            )]
        public void RemoveAuthorSucceedsForValidAuthorId()
        {
            authorManager.RemoveAuthor(validId);

            Assert.Throws<InvalidIdException<string>>(() => authorManager.GetAuthorById(validId));


        }

    }

   
}