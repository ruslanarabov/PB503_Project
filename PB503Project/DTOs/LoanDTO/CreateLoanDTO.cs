using System;
namespace PB503Project.DTOs.LoanDTO
{
	public class CreateLoanDTO
	{
		public int BorrowerId { get; set; }

		public List<int> BooksId { get; set; } //boriw olunanlaar

		public DateTime LoanDate { get; set; }

        public DateTime MustReturnDate { get; set; }
    }
}

