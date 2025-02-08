using System;
using PB503Project.DTOs.BorrowerDTO;
using PB503Project.DTOs.LoanDTO;
using PB503Project.Models;

namespace PB503Project.Services.Interfaces
{
	public interface ILoanService
	{
		void Add(CreateLoanDTO createLoanDTO);

		void Remove(int Id);

		void Update(int Id, UpdateLoanDTO updateLoanDTO);

		List<GetAllLoanDTO> GetAll();

        void BorrowBook(int borrowerId, int bookId);

        void ReturnBook(int loanId);

        Book GetMostBorrowedBook();
        
        List<Borrower> GetOverdueBorrowers();

        List<BorrowerHistoryDTO> GetBorrowerHistory(int borrowerId);
    }
}

