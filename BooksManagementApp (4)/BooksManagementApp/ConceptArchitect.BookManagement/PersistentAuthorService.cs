using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class PersistentAuthorService : IAuthorService
    {
        IRepository<Author, string> repository;

        //constructor based DI
        public PersistentAuthorService(IRepository<Author,string> repository)
        {
            this.repository = repository;
        }


        public async Task<Author> AddAuthor(Author author)
        {
            //perform some validation if needed
            if (author == null)
                throw new InvalidDataException("Author can't be null");

            if (string.IsNullOrEmpty(author.Id))
            {
                author.Id = await GenerateId(author.Name);
            }

            return await repository.Add(author);
        }

        private async Task<string> GenerateId(string name)
        {
            var id = name.ToLower().Replace(" ", "-");
            
            if (await repository.GetById(id) == null)
                return id;

            int d = 1;
            while (await repository.GetById($"{id}-{d}") != null)
                d++;

            return $"{id}-{d}";
            
        }

        public async Task DeleteAuthor(string authorId)
        {
            await repository.Delete(authorId);
        }

        public async Task< List<Author>> GetAllAuthors()
        {
            return await repository.GetAll();
        }

        public async Task<Author> GetAuthorById(string id)
        {
            return await repository.GetById(id);
        }

        public async Task<List<Author>> SearchAuthors(string term)
        {
            term = term.ToLower();

            return await repository.GetAll(a => a.Name.ToLower().Contains(term) || a.Biography.ToLower().Contains(term));
        }

        public async Task<Author> UpdateAuthor(Author author)
        {
            
            return await repository.Update(author, (old, newDetails) =>
            {
                old.Name = newDetails.Name;
                old.Email = newDetails.Email;
                old.Biography = newDetails.Biography;
                old.Photo=newDetails.Photo;
            });
        }
    }
}
