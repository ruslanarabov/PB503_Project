using System;
using PB503Project.AllExceptions;
using PB503Project.DTOs.AuthorDTO;
using PB503Project.Models;
using PB503Project.Repocitories.Impelemententions;
using PB503Project.Repocitories.Interfaces;
using PB503Project.Services.Interfaces;

namespace PB503Project.Services.Impelementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepocitory _authorRepocitory;

        public AuthorService() 
        {
            _authorRepocitory = new AuthorRepocitory();
        }

        public void Add(CreateAuthorDTO createAuthorDTO)
        {
            if (string.IsNullOrWhiteSpace(createAuthorDTO.Name)) throw new InvalidInputException("Author name cannot be empty.");
            var author = new Author
            {   Name = createAuthorDTO.Name,
                CreateAt = DateTime.UtcNow.AddHours(4),
                Books = new List<Book>()
            };
            _authorRepocitory.Add(author);
            _authorRepocitory.Commit();
        }


        public List<GetAllAuthorsDTO> GetAll()
        {
            var author = _authorRepocitory.GetAll();
            if (author.Any())
            {
                throw new InvalidInputException("There is no Author!");
            }
            return _authorRepocitory.GetAll().Select(author => new GetAllAuthorsDTO
            {
                Id = author.Id,
                Name = author.Name,
                BookTitles = author.Books.Select(b => b.Title).ToList()
            }).ToList();

        }

        public void Remove(int Id)
        {
            var author = _authorRepocitory.GetAll().FirstOrDefault(x => x.Id == Id);
            if (author is null)
            {
                throw new InvalidIdException("Author ID not found to delete!");
            }
            _authorRepocitory.Delete(author);
            _authorRepocitory.Commit();
        }

        public void Update(int Id, UpdateAuthorDTO updateAuthorDTO)
        {
            var author = _authorRepocitory.GetAll().FirstOrDefault(x => x.Id == Id);
            if (author is null || string.IsNullOrWhiteSpace(author.Name))
            {
                throw new InvalidIdException("Author ID not found to update!");
            }
            author.Name = updateAuthorDTO.Name;
            author.UpdateAt = DateTime.UtcNow.AddHours(4);
            _authorRepocitory.Commit();
        }
    }
}

