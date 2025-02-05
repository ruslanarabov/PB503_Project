using System;
using PB503Project.AllExceptions;
using PB503Project.DTOs.LoanDTO;
using PB503Project.Models;
using PB503Project.Repocitories.Impelemententions;
using PB503Project.Repocitories.Interfaces;
using PB503Project.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PB503Project.Services.Impelementations
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepocitory _loanRepocitory;

        public LoanService()
        {
            _loanRepocitory = new LoanRepocitory();
        }

        public void Add(CreateLoanDTO createLoanDTO)
        {
            if (createLoanDTO == null) throw new InvalidInputException("Input cannot be null");
            if (createLoanDTO.LoanDate > DateTime.UtcNow.AddHours(4))
                throw new InvalidInputException("Invalid loan date! You cannot borrow a book in the future.");
            Loan loans = new Loan
            {
                BorrowId = createLoanDTO.BorrowerId,
                LoanDate = createLoanDTO.LoanDate,
                MustReturnDate = createLoanDTO.LoanDate.AddDays(15),
                LoanItems = createLoanDTO.BooksId.Select(bookId => new LoanItem { BookId = bookId }).ToList()
            };

            _loanRepocitory.Add(loans);
            _loanRepocitory.Commit();
        }

        public List<GetAllLoanDTO> GetAll()
        {
            
            var loans = _loanRepocitory.GetAll();
            if (!loans.Any())
            {
                throw new InvalidInputException("There is no loan books!");
            }
            return loans.Select(loans => new GetAllLoanDTO
            {
                BorrowerId = loans.BorrowId,
                LoanDate = loans.LoanDate,
                MustReturnDate = loans.MustReturnDate,
                BookTitles = loans.LoanItems.Select(item => item.Book.Title).ToList()
            }).ToList();
        }


        public void Remove(int Id)
        {
            var loans = _loanRepocitory.GetAll().FirstOrDefault(x => x.Id == Id);
            if (loans == null)
                throw new InvalidIdException("Id not found to delete!");
            _loanRepocitory.Delete(loans);
            _loanRepocitory.Commit();
        }

        public void Update(int Id, UpdateLoanDTO updateLoanDTO)
        {
            var loans = _loanRepocitory.GetAll().FirstOrDefault(x => x.Id == Id);
            if (loans == null)
                throw new InvalidIdException("Id not found to update!");
            loans.ReturnTime = DateTime.UtcNow.AddHours(4);
            _loanRepocitory.Commit();
        }
    }
}

