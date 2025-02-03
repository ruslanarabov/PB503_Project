using System;
using PB503Project.AllExceptions;
using PB503Project.DTOs.BookDTO;
using PB503Project.Models;
using PB503Project.Repocitories.Impelemententions;
using PB503Project.Repocitories.Interfaces;
using PB503Project.Services.Interfaces;

namespace PB503Project.Services.Impelementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepocitory _bookRepocitory;
        private readonly IAuthorRepocitory _authorRepocitory;

        public BookService()
        {
            _bookRepocitory = new BookRepocitory();
            _authorRepocitory = new AuthorRepocitory();
        }

        public void Add(CreateBookDTO createBookDTO)
        {
            var authors = _authorRepocitory.GetAll().Where(a => createBookDTO.AuthorsId.Contains(a.Id)).ToList();
            var book = new Book
            {
                Title = createBookDTO.Title,
                Desc = createBookDTO.Description,
                PublishYear = createBookDTO.PublishYear,
                Authors = authors
            };
            _bookRepocitory.Add(book);
            _bookRepocitory.Commit();
        }

        public List<GetAllBooksDTO> GetAll()
        {
            var book = _bookRepocitory.GetAll();
            if (book.Any())
            {
                throw new InvalidInputException("There is no Book!");
            }
            return _bookRepocitory.GetAll().Select(book => new GetAllBooksDTO
            {
                Id = book.Id,
                Title = book.Title,
                Descriptions = book.Desc,
                PublishYear = book.PublishYear,
                Authors = book.Authors.Select(a => a.Name).ToList(),
                IsBorow = book.LoanItem != null
            }).ToList();
        }

        public void Remove(int Id)
        {
            var book = _bookRepocitory.GetAll().FirstOrDefault(x => x.Id == Id);
            if (book is null)
            {
                throw new InvalidIdException("Id not found to delete!");
            }
            _bookRepocitory.Delete(book);
            _bookRepocitory.Commit();
        }

        public void Update(int Id, UpdateBookDTO updateBookDTO)
        {
            var book = _bookRepocitory.GetAll().FirstOrDefault(b => b.Id == Id);
            if (book is null)
            {
                throw new InvalidIdException("Id not found to update!");
            }

            var authors = _authorRepocitory.GetAll().Where(a => updateBookDTO.AuthorsId.Contains(a.Id)).ToList();

            book.Title = updateBookDTO.Title;

            book.Desc = updateBookDTO.Descriptions;

            book.Authors = authors;

            book.PublishYear = updateBookDTO.PublishYear;

            _bookRepocitory.Commit();
        }
    }
}

