using System;
using PB503Project.AllExceptions;
using PB503Project.DTOs.BorrowerDTO;
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
        private readonly IBorrowerRepocitory _borrowerRepocitory;
        private readonly IBookRepocitory _bookRepocitory;
        private readonly ILoanItemRepocitory _loanItemRepocitory;

        public LoanService()
        {
            _loanRepocitory = new LoanRepocitory();
            _borrowerRepocitory = new BorrowerRepocitory();
            _bookRepocitory = new BookRepocitory();
            _loanItemRepocitory = new LoanItemRepocitory();
        }

        public void Add(CreateLoanDTO createLoanDTO)
        {
            var loan = new Loan { BorrowId = createLoanDTO.BorrowerId };
            foreach (var bookId in createLoanDTO.BooksId)
            {
                loan.LoanItems.Add(new LoanItem { BookId = bookId });
            }
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

        //public void BorrowBook(int borrowerId, int bookId)
        //{
        //    var borrower = _borrowerRepocitory.GetById(borrowerId);
        //    if (borrower == null)
        //    {
        //        throw new InvalidIdException("Borrower not found!");
        //    }

        //    var book = _bookRepocitory.GetById(bookId);
        //    if (book == null)
        //    {
        //        throw new InvalidIdException("Book not found!");
        //    }


        //    var existingLoan = _loanRepocitory.GetAll()
        //        .FirstOrDefault(l => l.BorrowId == borrowerId && l.BookId == bookId && l.ReturnTime == null);

        //    if (existingLoan != null)
        //    {
        //        throw new InvalidOperationException("This book is already borrowed by this borrower and has't been returned.");
        //    }


        //    var newLoan = new Loan
        //    {
        //        BorrowId = borrowerId,
        //        BookId = bookId,
        //        BorrowDate = DateTime.UtcNow.AddHours(4),
        //        ReturnTime = null
        //    };

        //    _loanRepocitory.Add(newLoan);
        //    _loanRepocitory.Commit();
        //}


        public void BorrowBook(int borrowerId, int bookId)
        {
            var borrower = _borrowerRepocitory.GetById(borrowerId);
            if (borrower == null)
            {
                throw new InvalidIdException("Borrower not found!");
            }

            var book = _bookRepocitory.GetById(bookId);
            if (book == null)
            {
                throw new InvalidIdException("Book not found!");
            }

            var existingLoan = _loanRepocitory.GetAll()
                .FirstOrDefault(l => l.BorrowId == borrowerId && l.BookId == bookId && l.ReturnTime == null);

            if (existingLoan != null)
            {
                throw new InvalidOperationException("This book is already borrowed by this borrower and hasn't been returned.");
            }

            
            var newLoan = new Loan
            {
                BorrowId = borrowerId,
                BorrowDate = DateTime.UtcNow.AddHours(4),
                MustReturnDate = DateTime.UtcNow.AddHours(4).AddDays(15), 
                ReturnTime = null
            };

            _loanRepocitory.Add(newLoan);
            _loanRepocitory.Commit();

            
            var loanItem = new LoanItem
            {
                BookId = bookId,
                LoanId = newLoan.Id,
                Loan = newLoan,
                Book = book
            };

            _loanItemRepocitory.Add(loanItem);
            _loanItemRepocitory.Commit();
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

        public void ReturnBook(int loanId)
        {
            var loan = _loanRepocitory.GetById(loanId);
            if (loan == null)
            {
                throw new InvalidIdException("Loan not found!");
            }

            loan.ReturnTime = DateTime.UtcNow.AddHours(4);
            _loanRepocitory.Commit();
        }

   
        public Book GetMostBorrowedBook()
        {
            var mostBorrowedBook = _loanRepocitory.GetAll()
                .GroupBy(l => l.BookId)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            if (mostBorrowedBook == null)
            {
                throw new InvalidOperationException("No borrowed books found.");
            }

            var book = _bookRepocitory.GetById(mostBorrowedBook.Key);
            return book;
        }

        
        public List<Borrower> GetOverdueBorrowers()
        {
            var overdueLoans = _loanRepocitory.GetAll()
                .Where(l => l.MustReturnDate < DateTime.UtcNow.AddHours(4) && l.ReturnTime == null)
                .ToList();

            var overdueBorrowers = overdueLoans
                .Select(l => _borrowerRepocitory.GetById(l.BorrowId))
                .Where(b => b != null)
                .ToList();

            return overdueBorrowers;
        }

        
        public List<BorrowerHistoryDTO> GetBorrowerHistory(int borrowerId)
        {
            var borrowerLoans = _loanRepocitory.GetAll()
                .Where(l => l.BorrowId == borrowerId)
                .ToList();

            var history = borrowerLoans
                .Select(l => new BorrowerHistoryDTO
                {
                    BookTitle = _bookRepocitory.GetById(l.BookId)?.Title,
                    BorrowDate = l.BorrowDate,
                    ReturnDate = l.ReturnTime
                })
                .ToList();

            return history;
        }





    }
}

