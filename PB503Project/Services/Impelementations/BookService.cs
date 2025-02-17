﻿using System;
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
        private readonly ILoanRepocitory _loanRepocitory;

        public BookService()
        {
            _bookRepocitory = new BookRepocitory();
            _authorRepocitory = new AuthorRepocitory();
            _loanRepocitory = new LoanRepocitory();
        }

        public void Add(CreateBookDTO createBookDTO)
        {
            if (createBookDTO.AuthorsId == null || !createBookDTO.AuthorsId.Any())
            {
                throw new ArgumentException("Invalid Author ID!");
            }
            if (string.IsNullOrWhiteSpace(createBookDTO.Title))
                throw new InvalidInputException("Book name cannot be empty.");

            var authors = _authorRepocitory.GetAll()
                .Where(a => createBookDTO.AuthorsId.Contains(a.Id))
                .ToList();
            if (!authors.Any())
            {
                throw new InvalidInputException("Author not found!");
            }
            var book = new Book
            {
                Title = createBookDTO.Title,
                Desc = createBookDTO.Description,
                PublishYear = createBookDTO.PublishYear,
                CreateAt = DateTime.UtcNow.AddHours(4),
                Authors = new List<Author>()
            };

            _bookRepocitory.Add(book);
            _bookRepocitory.Commit();
        }

        public List<GetAllBooksDTO> GetAll()
        {
            var book = _bookRepocitory.GetAll();
            if (!book.Any())
            {
                throw new InvalidInputException("There are no books available!");
            }
            return _bookRepocitory.GetAll().Select(book => new GetAllBooksDTO
            {
                Id = book.Id,
                Title = book.Title,
                Descriptions = book.Desc,
                PublishYear = book.PublishYear,
                Authors = book.Authors != null && book.Authors.Any() ? book.Authors.Select(a => a.Name).ToList() : new List<string> { "No Author" },
                IsBorow = !_loanRepocitory.GetAll().Any(l => l.BookId == book.Id && l.ReturnTime == null)

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
            if (book is null || string.IsNullOrWhiteSpace(book.Title))
            {
                throw new InvalidIdException("Id not found to update!");
            }

            var authors = _authorRepocitory.GetAll().Where(a => updateBookDTO.AuthorsId.Contains(a.Id)).ToList();

            book.Title = updateBookDTO.Title;

            book.Desc = updateBookDTO.Descriptions;

            book.Authors = authors;

            book.PublishYear = updateBookDTO.PublishYear;

            book.UpdateAt = DateTime.UtcNow.AddHours(4);

            _bookRepocitory.Commit();
        }
    }
}

